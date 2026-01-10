using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData; 
    protected IMovable MovableComponent;

    public void Start()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(characterData);

    }
    public abstract void Update();
}
