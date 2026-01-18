using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacter : Character
{
    //---- Properties ----
    public override Character CharacterTarget {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].CharacterType == CharacterType.Player)
                    continue;

                if (!list[i].LiveComponent.IsAlive)
                    continue;

                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if(distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                }
            }
            return target; 
        } 
    }

    //---- Functions ----
    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new PlayerLiveComponent();
        LiveComponent.Initialize(this);

        AttackComponent = new CharacterAttackComponent();
        AttackComponent.Initialize(this);

        InputComponent = new PlayerInputComponent();
    }

    public override void Update()
    {
        if (!LiveComponent.IsAlive || !GameManager.Instance.IsGameActive)
            return;

        Vector3 moveDirection = InputComponent.GetMoveDirection();

        //Update Attack Cooldown Timer
        if (AttackComponent.AttackCooldownTimer > 0)
        {
            AttackComponent.AttackCooldownTimer -= Time.deltaTime;
        }

        if (CharacterTarget == null)
        {
            MovableComponent.Rotation(moveDirection);
        }
        else
        {
            Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
            MovableComponent.Rotation(rotationDirection);

            if (Input.GetButtonDown("Jump"))
            {
                //Check Distance to Target
                float distanceToTarget = Vector3.Distance(CharacterTarget.transform.position, transform.position);
                if (distanceToTarget <= AttackComponent.AttackRange && AttackComponent.AttackCooldownTimer <= 0)
                {
                    AttackComponent.DoDamage(CharacterTarget);
                }
            }
        }

        MovableComponent.Move(moveDirection);
    }
}