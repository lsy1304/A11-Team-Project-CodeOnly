using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Enemy.NavMeshAgent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Enemy.NavMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        base.Update();

        if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        stateMachine.Enemy.NavMeshAgent.SetDestination(stateMachine.Target.transform.position);
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }
}
