using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")] 
public class GameData : ScriptableObject
{
    [SerializeField] private int sessionTimeMinutes = 15;
    
    [Space(10), Header("Experience progress")]
    [SerializeField]
    private int baseExperience = 20;
    [SerializeField]
    private int grownRate = 10;

    [Space(10), Header("SpawnLogic")]
    [SerializeField] private float timeBetweenEnemySpawn = 3.0f;
    [SerializeField] private float minSpawnOffset = 7;
    [SerializeField] private float maxSpawnOffset = 18;

    public int SessionTimeMinutes => sessionTimeMinutes;
    public int SessionTimeSeconds => sessionTimeMinutes * 60;
    public int BaseExperience => baseExperience;
    public int GrownRate => grownRate;
    public float TimeBetweenEnemySpawn => timeBetweenEnemySpawn; 
    public float MinSpawnOffset => minSpawnOffset;
    public float MaxSpawnOffset => maxSpawnOffset;
}
