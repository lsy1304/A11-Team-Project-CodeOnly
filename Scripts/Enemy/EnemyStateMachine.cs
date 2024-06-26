using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }


    public float MovementSpeedModifier { get; set; }
    public float MovementSpeed { get; set; } = 3.0f;
    public float RotationDamping { get; set; } = 5.0f; // ȸ�� ���� ����
    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        ChangeState(IdleState);
    }

    public bool IsInChaseRange()
    {
        float distanceToPlayerSqr = (Target.transform.position - Enemy.transform.position).sqrMagnitude;
        return distanceToPlayerSqr <= Enemy.Data.PlayerChasingRange * Enemy.Data.PlayerChasingRange;
    }

    public bool IsInAttackRange()
    {
        float distanceToPlayerSqr = (Target.transform.position - Enemy.transform.position).sqrMagnitude;
        return distanceToPlayerSqr <= Enemy.Data.AttackRange * Enemy.Data.AttackRange;
    }
}
