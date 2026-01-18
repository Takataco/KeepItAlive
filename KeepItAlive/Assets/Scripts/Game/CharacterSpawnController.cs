using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField] private CharacterFactory characterFactory;
    public CharacterFactory CharacterFactory => characterFactory;

    public void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerposition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerposition.x + GetOffset(), 0, playerposition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += GameManager.Instance.CharacterDeathHandler;

        float GetOffset()
        {
            // 50/50 chance to get either result 
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(GameManager.Instance.GameData.MinSpawnOffset, GameManager.Instance.GameData.MaxSpawnOffset);
            return (isPlus) ? offset : (-1 * offset);
        }
    }
}
