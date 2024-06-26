using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    private float updateInterval = 3f;
    private float timeSinceLastUpdate;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        //stateMachine.Enemy.NavMeshAgent.isStopped = true;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
        timeSinceLastUpdate = updateInterval;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //if (!stateMachine.Enemy.NavMeshAgent.isStopped)
        //{
            // 네비게이션 메시를 사용하여 적을 이동시킵니다.
        //   Vector3 randomPosition = GetRandomPositionOnNavMesh();
        //    stateMachine.Enemy.NavMeshAgent.SetDestination(randomPosition);
        //    stateMachine.Enemy.NavMeshAgent.isStopped = true; // 다음에 아이들 상태로 돌아오면 멈추도록 설정합니다.
        //}

        if (IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += stateMachine.Enemy.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return stateMachine.Enemy.transform.position;
        }
    }
}
