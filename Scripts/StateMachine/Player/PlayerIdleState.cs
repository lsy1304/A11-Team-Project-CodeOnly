using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if(stateMachine.MovementInput != Vector2.zero)
        {
            if (stateMachine.Player.Input.playerAction.Run.IsPressed()) stateMachine.ChangeState(stateMachine.RunState);
            else stateMachine.ChangeState(stateMachine.WalkState);
        }

        else if (stateMachine.IsAttacking)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
