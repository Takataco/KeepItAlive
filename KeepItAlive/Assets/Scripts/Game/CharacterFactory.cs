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

    //---- Properties ----
    public Character Player
    {
        get; private set;
    }

    public List<Character> ActiveCharacters => activeCharacters;
    
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
        return character;
    }

    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacers[character.CharacterType];
        characters.Enqueue(character); 
        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Player:
                character = GameObject.Instantiate(playerCharacterPrefab, null);

                break;
            case CharacterType.DefaultEnemy:
                character = GameObject.Instantiate(enemyCharacterPrefab, null);
                Player = character;
                break;
            default:
                Debug.LogError("Unkown character type :" + type);
                break;
        }
        return character;
    }
}
