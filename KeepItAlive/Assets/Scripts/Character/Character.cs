using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] protected CharacterData characterData; 
    protected IMovable MovableComponent;

    //---- Functions ----
    public void Start()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(characterData);

    }
    public abstract void Update();
}
