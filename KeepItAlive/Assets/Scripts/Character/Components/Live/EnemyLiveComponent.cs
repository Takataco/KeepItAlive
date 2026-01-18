using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLiveComponent : ILiveComponent
{
    //---- Attributes ----
    private Character selfCharacter;
    public event Action<Character> OnCharacterDeath;

    private float health = 10;

    //---- Properties ----
    public bool IsAlive => health > 0;
    public int MaxHealth => 10;
    public float Health
    {
        get => health;
        private set
        {
            health = value;
            if (health < 0)
            {
                health = 0;
                SetDeath();
            }
        }
    }

    //---- Functions ----
    public void TakeDamage(float damage)
    {
        Health -= damage * 1000;
    }

    private void SetDeath()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
    }
}
