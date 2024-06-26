public class PlayerAttackState : PlayerBaseState
{
    bool alreadyAppliedDealing;
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if( !alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.AttackData.Dealing_Start_TrasisitonTime)
        {
            alreadyAppliedDealing = true;
            stateMachine.Player.weapon.gameObject.SetActive(true);
        }
        if(alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.AttackData.Dealing_End_TrasisitonTime)
        {
            alreadyAppliedDealing = false;
            stateMachine.Player.weapon.gameObject.SetActive(false);
        }
        if(normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
