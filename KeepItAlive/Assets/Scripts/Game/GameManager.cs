using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private CharacterSpawnController characterSpawnController;
    [SerializeField] private WindowsService windowsService;

    private bool isGameActive = false;
    private bool isGamePaused = true;
    private float gameSessionTime;
    private float timeBetweenEnemySpawn;

    //---- Properties ----
    public static GameManager Instance { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public WindowsService WindowsService => windowsService;
    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterSpawnController CharacterSpawnController => characterSpawnController;
    public GameData GameData => gameData;
    public bool IsGameActive => isGameActive;
    public float GameSessionTime => gameSessionTime;
    public int MaxEnemyNumber
    {
        get
        {
            //Exponential formula (basenum * Mathf.Pow(exponent, wave)); 
            //Сперва будет 5 потом 7 потом 11 потом 16 и тд 
            return Mathf.FloorToInt(5 * Mathf.Pow(1.5f, (((int)(gameSessionTime) / 60))));
        }
    }

    public bool IsGamePaused
    {
        get => isGamePaused;
        set
        {
            isGamePaused = value;
        }
    }

    //---- Functions ----
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //The load of a new Scene destroys all current Scene objects.
            //this preserves an Object during scene loading
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            //destroy other game manager if one already exists 
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        ScoreManager = new ScoreManager();
        windowsService.Initialize();
    }

    //method to start game 
    public void StartGame()
    {
        if (isGameActive)
        {
            Debug.Log("Game is already active");
            return;
        }

        //create a character 
        var player = characterFactory.GetCharacter(CharacterType.Player);
        //spawn point of player character
        player.transform.position = Vector3.zero;
        // Turns on the game object of the player 
        player.gameObject.SetActive(true);
        player.Initialize();
        // Subscribing a function to the OnCharDeath event using +=
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0f;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        ScoreManager.StartGame();
        isGameActive = true;
        IsGamePaused = false;

    }

    private void Update()
    {
        if (!isGameActive || IsGamePaused)
        {
            return;
        }

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn += Time.deltaTime;

        //сюда добавить проверку на maxenemynumber
        if (timeBetweenEnemySpawn >= gameData.TimeBetweenEnemySpawn && characterFactory.ActiveEnemyNumber < MaxEnemyNumber)
        {
            CharacterSpawnController.SpawnEnemy();
            Debug.Log("Enemy spawned, current MaxEnemyNumber: " + MaxEnemyNumber + "current activeEnemyNumber: " + characterFactory.ActiveEnemyNumber);
            timeBetweenEnemySpawn = 0;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    public void CharacterDeathHandler(Character deadCharacter)
    {
        Debug.Log("character " + deadCharacter.gameObject.name + " is dead");
        switch (deadCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                ScoreManager.CharacterDeathHandler(deadCharacter);
                break;
        }

        characterFactory.ReturnCharacter(deadCharacter);
        deadCharacter.gameObject.SetActive(false);

        // Unsubscribing a function to the OnCharDeath event using -=
        deadCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void GameVictory()
    {
        ScoreManager.CompleteMatch();
        isGameActive = false;
        IsGamePaused = true;
        WindowsService.HideWindow<GameplayWindow>(true);
        WindowsService.ShowWindow<VictoryWindow>(false);
    }

    private void GameOver()
    {
        Debug.Log("Defeat");
        ScoreManager.CompleteMatch();
        isGameActive = false;
        IsGamePaused = true;
        WindowsService.HideWindow<GameplayWindow>(true);
        WindowsService.ShowWindow<DefeatWindow>(false);
    }
}