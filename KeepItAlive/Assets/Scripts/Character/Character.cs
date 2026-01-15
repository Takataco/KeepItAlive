using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //---- Attributes ----
    [SerializeField] protected CharacterData characterData;

    //---- Properties ----
    public IMovable MovableComponent {  get; protected set; }
    public  ILiveComponent LiveComponent {  get; protected set; }
    public IAttackComponent AttackComponent { get; protected set; }

    //---- Functions ----
    public virtual void Start()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(characterData);

    }
    public abstract void Update();
}
