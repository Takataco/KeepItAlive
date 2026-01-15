using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : IAttackComponent
{
    private CharacterData characterData;
    private float attackCooldown = 3;
    private float attackCooldownTimer = 3;
    public float Damage => 5;
    public float AttackRange => 3.0f;
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
