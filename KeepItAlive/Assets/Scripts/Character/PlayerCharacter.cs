using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : Character
{
    //---- Functions ----
    public override void Start()
    {
        base.Start();
        LiveComponent = new PlayerLiveComponent();
        InputComponent = new PlayerInputComponent();
    }

    public override void Update()
    {
        if (!LiveComponent.IsAlive)
            return;

        Vector3 moveDirection = InputComponent.GetMoveDirection();

        MovableComponent.Move(moveDirection);
        MovableComponent.Rotation(moveDirection);
    }
}
