using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacers
        = new Dictionary<CharacterType, Queue<Character>>();

    private List<Character> activeCharacters = new List<Character>();
    private int activeEnemyNumber = 0;

    //---- Properties ----
    public Character Player
    {
        get; private set;
    }

    public List<Character> ActiveCharacters => activeCharacters;
    public int ActiveEnemyNumber => activeEnemyNumber;

    //---- Functions ----
    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (disabledCharacers.ContainsKey(type))
        {
            if (disabledCharacers[type].Count > 0)
            {
                character = disabledCharacers[type].Dequeue();
            }
        }
        else
        {
            disabledCharacers.Add(type, new Queue<Character>());
        }

        if (character == null)
        {
            character = InstantiateCharacter(type);
        }
        
        activeCharacters.Add(character);

        if (character.CharacterType == CharacterType.DefaultEnemy)
        {
            activeEnemyNumber++;
        }

        return character;
    }

    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacers[character.CharacterType];
        characters.Enqueue(character);
        if (character.CharacterType == CharacterType.DefaultEnemy)
        {
            activeEnemyNumber--;
        }
        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Player:
                character = GameObject.Instantiate(playerCharacterPrefab, null);
                Debug.Log("Character with following type created :" + type);
                Player = character;
                break;
            case CharacterType.DefaultEnemy:
                character = GameObject.Instantiate(enemyCharacterPrefab, null); 
                Debug.Log("Character with following type created :" + type);
                break;
            default:
                Debug.LogError("Unknown character type :" + type);
                break;
        }
        return character;
    }
}
