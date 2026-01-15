using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : IAttackComponent
{
    private Transform characterTransform; 
    private float lockDamageTime = 0;
    //private float attackRange = 5;
    public float Damage => 5;

    public void Initialize(CharacterData characterData)
    {
        characterTransform = characterData.CharacterTransform; 
    }

    public void DoDamage(Character target)
    {
        if (target == null) 
            return;

        if (!target.LiveComponent.IsAlive) 
            return;

        if (Vector3.Distance(target.transform.position, characterTransform.position) > 3)
            return;

        if (lockDamageTime > 0)
        {
            lockDamageTime -= Time.deltaTime;
            return; 
        }
        target.LiveComponent.TakeDamage(Damage);
        lockDamageTime = 1; 
    }
}
