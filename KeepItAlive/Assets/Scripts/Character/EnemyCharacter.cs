using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    // ---- Attributes ----
    [SerializeField] private AiState aiState;
    public override Character CharacterTarget => GameManager.Instance.CharacterFactory.Player; 

    //----Functions----
    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new EnemyLiveComponent();
        AttackComponent = new EnemyAttackComponent();
        AttackComponent.Initialize(this);
        InputComponent = new EnemyInputComponent(transform, CharacterTarget.transform);
    }

    //for testing purposes 
    private void OnDrawGizmos()
    {
        if (AttackComponent == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackComponent.AttackRange);
    }

    public override void Update()
    {
        //Check if Enemy is Alive
        if (LiveComponent.Health <= 0)
            return;

        //Update Attack Cooldown Timer
        if (AttackComponent.AttackCooldownTimer > 0)
        {
            AttackComponent.AttackCooldownTimer -= Time.deltaTime;
        }

        //Check Distance to Player
        float distanceToPlayer = Vector3.Distance(CharacterTarget.transform.position, transform.position);
        
        //State Machine Logic
        switch (aiState)
        {
            case AiState.None:
                return;
            case AiState.MoveToTarget:
                if(distanceToPlayer <= AttackComponent.AttackRange && AttackComponent.AttackCooldownTimer <= 0)
                {
                    AttackComponent.AttackCooldownTimer = AttackComponent.AttackCooldown;
                    aiState = AiState.Attack;
                }
                else
                {
                    Vector3 moveDirection = InputComponent.GetMoveDirection();
                    MovableComponent.Move(moveDirection);
                    MovableComponent.Rotation(moveDirection);
                }
                return;
            case AiState.Attack:
                AttackComponent.DoDamage(CharacterTarget);
                aiState = AiState.MoveToTarget;
                return;
        }
    }
}
