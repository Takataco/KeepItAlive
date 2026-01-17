using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] private CharacterType characterType;
    [SerializeField] protected CharacterData characterData;

    //---- Properties ----
    public virtual Character CharacterTarget { get; }
    public CharacterType CharacterType => CharacterType;
    public IMovable MovableComponent {  get; protected set; }
    public  ILiveComponent LiveComponent {  get; protected set; }
    public IAttackComponent AttackComponent { get; protected set; }
    public IInputComponent InputComponent;
    public CharacterData CharacterData => characterData;

    //---- Functions ----
    public virtual void Initialize()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(this);
    }
    public abstract void Update();
}
