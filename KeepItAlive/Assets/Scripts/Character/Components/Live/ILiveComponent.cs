using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent
{
    //---- Attributes ----
    public event Action OnDeath;

    //---- Properties ----
    public int MaxHealth { get;}
    public float Health { get; }
    public bool IsAlive { get; }

    //---- Functions ----
    public void TakeDamage (float damage);
}
