using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    private ScoreSystem scoreSystem;
    private bool isGameActive;
    private float gameSessionTime;
    private float timeBetweenEnemySpawn; 

    //---- Properties ----
    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;

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

        if (timeBetweenEnemySpawn <= 0)
        {
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;
            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deadCharacter.CharacterData.ScoreCost);
                break;
        }

        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);

    }
    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerposition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerposition.x + GetOffset(), 0, playerposition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();

        float GetOffset()
        {
            // 50/50 chance to get either result 
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return (isPlus) ? offset : (-1 * offset);
        }
    }

    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("Vicotry");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        Debug.Log("Defeat");
        isGameActive = false;
    }
}