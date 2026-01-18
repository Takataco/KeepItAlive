using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private CharacterSpawnController characterSpawnController;

    private ScoreSystem scoreSystem;
    private bool isGameActive;
    private float gameSessionTime;
    private float timeBetweenEnemySpawn;

    //---- Properties ----
    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;
    public CharacterSpawnController CharacterSpawnController => characterSpawnController;
    public GameData GameData => gameData;
    public bool IsGameActive => isGameActive;
    public int MaxEnemyNumber
    {
        get
        {
            //Exponential formula (basenum * Mathf.Pow(exponent, wave)); 
            //Сперва будет 5 потом 7 потом 11 потом 16 и тд 
            return Mathf.FloorToInt(5 * Mathf.Pow(1.5f, (((int)(gameSessionTime) / 60))));
        }
    }

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
        scoreSystem = new ScoreSystem();
        isGameActive = false; 
    }

    //method to start game 
    public void StartGame()
    {
        if (isGameActive)
            return; 

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        //spawn point of player character
        player.transform.position = Vector3.zero;
        // Turns on the game object of the player 
        player.gameObject.SetActive(true);
        player.Initialize();
        // Subscribing a function to the OnCharDeath event using +=
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        scoreSystem.StartGame();

        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive)
        {
            return;
        }

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        //сюда добавить проверку на maxenemynumber
        if (timeBetweenEnemySpawn <= 0 && characterFactory.ActiveEnemyNumber < MaxEnemyNumber)
        {
            CharacterSpawnController.SpawnEnemy();
            Debug.Log("Enemy spawned, current MaxEnemyNumber: " + MaxEnemyNumber + "current activeEnemyNumber: " + characterFactory.ActiveEnemyNumber);
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    public void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.CharacterType)
        {
            case CharacterType.Player:
                Debug.Log("Calling GameOver()");
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deadCharacter.CharacterData.ScoreCost);
                break;
        }

        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);

        // Unsubscribing a function to the OnCharDeath event using -=
        deadCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
    }

    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("Vicotry");
        isGameActive = false;
    }

    private void GameOver()
    {
        Debug.Log("Defeat");
        scoreSystem.EndGame();
        isGameActive = false;
    }
}