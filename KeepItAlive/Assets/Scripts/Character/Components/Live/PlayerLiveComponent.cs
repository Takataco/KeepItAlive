using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiveComponent : ILiveComponent
{
    //---- Attributes ----
    public event Action OnDeath;
    private float health = 50;

    //---- Properties ----
    public bool IsAlive => health > 0;
    public int MaxHealth => 50;
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
        Health -= damage;
        Debug.Log($"Player took {damage} damage. {Health} health left.");
    }

    private void SetDeath()
    {
        OnDeath?.Invoke();
    }
}
