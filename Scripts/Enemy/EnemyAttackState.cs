using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedForce;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

        base.Enter();
        stateMachine.Enemy.NavMeshAgent.isStopped = true;
        stateMachine.Enemy.Animator.SetTrigger("Attack");
        Debug.Log("Entering Attack State");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        alreadyAppliedForce = false;
        Debug.Log("Exiting Attack State");
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();
        }
        else
        {
            if (IsInChaseRange())
                stateMachine.ChangeState(stateMachine.ChasingState);
            else
                stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enemy.Controller.Move(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);
    }
}
