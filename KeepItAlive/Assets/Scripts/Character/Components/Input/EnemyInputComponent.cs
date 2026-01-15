using UnityEngine;

public class EnemyInputComponent : IInputComponent
{
    private Transform target;
    private Transform selfTransform;

    public EnemyInputComponent(Transform selfTransform, Transform target)
    {
        this.selfTransform = selfTransform;
        this.target = target;
    }
    public Vector3 GetMoveDirection()
    {
        if (target == null)
        {
            return Vector3.zero;
        }

        Vector3 moveDirection = target.position - selfTransform.position;
        return moveDirection.normalized;
    }
}
