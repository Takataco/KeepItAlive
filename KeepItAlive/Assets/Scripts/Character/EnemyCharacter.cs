using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    // ---- Attributes ----
    [SerializeField] private AiState aiState;
    [SerializeField] private Character targetCharacter;

    //----Functions----
    public override void Start()
    {
        base.Start();
        LiveComponent = new EnemyLiveComponent();
    }

    public override void Update()
    {
        switch (aiState)
        {
            case AiState.None:
                return;
            case AiState.MoveToTarget:
                Move();
                return;
        }
    }

    private void Move()
    {
        if (targetCharacter == null) 
            return; 

        Vector3 direction = targetCharacter.transform.position - characterData.CharacterTransform.position;
        direction = direction.normalized;

        MovableComponent.Move(direction);
        MovableComponent.Rotation(direction);
    }

}
