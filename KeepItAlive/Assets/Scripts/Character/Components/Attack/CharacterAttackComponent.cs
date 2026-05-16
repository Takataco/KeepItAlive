using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComponent : IAttackComponent
{
    //---- Attributes ----
    private CharacterData characterData;
    private float attackCooldown = 2;
    private float attackCooldownTimer = 2;

    //---- Properties ----
    public float Damage => 10;
    public float AttackRange => 5.0f;
    public float AttackCooldown => attackCooldown;
    public float AttackCooldownTimer
    {
        get
        {
            return attackCooldownTimer;
        }
        set
        {
            attackCooldownTimer = Mathf.Clamp(value, 0, attackCooldown);
        }
    }

    //---- Functions ----
    public void Initialize(Character character)
    {
        this.characterData = character.CharacterData; 
    }

    public void DoDamage(Character target)
    {
        if (target == null) 
            return;

        if (!target.LiveComponent.IsAlive) 
            return;

        if (Vector3.Distance(characterData.CharacterTransform.position, target.transform.position) <= AttackRange)
        {
            target.LiveComponent.TakeDamage(Damage);
        }
    }
}
