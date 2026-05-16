using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс ScoreManager будет ответственным за подсчет наших игровых очков, а также за их
// загрузку и хранение.
public class ScoreManager
{
    //---- Attributes ----
    private const string SESSION_SCORE_MAX = "save_score_max";
    private const string CURRENT_SCORE = "save_current_score";

    //score of the current run
    private int gameScore;
    //sum of all scores over all runs 
    private int globalGameScore;
    //max score over all runs
    private int scoreMax;

    public event Action<int> OnScoreUpdated;
    public event Action<int> OnSessionScoreUpdated;
    public event Action<int> OnScoreChanged;
    
    //---- Properties ----
    public int GameScore => gameScore;
    public int GlobalGameScore
    {
        get => globalGameScore;
        set
        {
            globalGameScore = value;
            if (globalGameScore < 0)
                globalGameScore = 0;

            PlayerPrefs.SetInt(CURRENT_SCORE, globalGameScore);
        }
    }
    public int ScoreMax => scoreMax;
    public bool IsNewScoreRecord { get; private set; }

    //---- Functions ----
    public ScoreManager()
    {
        // PlayerPrefs предоставляет простой способ хранения данных вашей игры.
        gameScore = 0;
        scoreMax = PlayerPrefs.GetInt(SESSION_SCORE_MAX, 0);
        globalGameScore = PlayerPrefs.GetInt(CURRENT_SCORE, 0);
        IsNewScoreRecord = false;
    }
    public void StartGame() {
        gameScore = 0;
        IsNewScoreRecord = false;
    }

    /* old???
    public void AddScore(int scoreCost)
    {
        gameScore += scoreCost;
        OnScoreChanged?.Invoke(gameScore);

        // if current run score is higher than max score, make it the new max score
        if (gameScore <= scoreMax)
            return;

        scoreMax = GameScore;
        PlayerPrefs.SetInt(SESSION_SCORE_MAX, scoreMax);
        IsNewScoreRecord = true;
    }
    */

    public void CompleteMatch()
    {
        GlobalGameScore += gameScore;
    }
    
    //added 
    public void CharacterDeathHandler(Character character)
    {
        gameScore += character.CharacterData.ScoreCost; // add score of enemy to you
        OnScoreChanged?.Invoke(gameScore);

        if (gameScore <= scoreMax)
            return;

        scoreMax = GameScore;
        PlayerPrefs.SetInt(SESSION_SCORE_MAX, scoreMax);
        IsNewScoreRecord = true;
    }
}
