using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackComponent
{
    //---- Properties ----
    public float Damage { get;}
    float AttackRange { get; }
    float AttackCooldown { get; }
    float AttackCooldownTimer { get; set; }

    //---- Functions ----
    public void Initialize(Character character);
    public void DoDamage(Character target);
    
}
