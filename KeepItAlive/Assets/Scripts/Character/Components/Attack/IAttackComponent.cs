using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackComponent
{
    //---- Properties ----
    public float Damage { get;}

    //---- Functions ----
    public void Initialize(CharacterData characterData);
    public void DoDamage(Character target);
    
}
