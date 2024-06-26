using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation( stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        CharacterController charaCollider = stateMachine.Player.Controller;
        Ray ray = new Ray();
        ray.origin = charaCollider.transform.position + (charaCollider.center * 1.4f) + charaCollider.transform.forward * (charaCollider.radius + .01f);
        ray.direction = charaCollider.transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * .5f);
        base.Update();
        if(stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    private void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.AttackState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero) return;

        stateMachine.MovementInput = Vector2.zero;
        stateMachine.ChangeState(stateMachine.IdleState);
        base.OnMovementCanceled(context);
    }

    protected override void OnInteractStart(InputAction.CallbackContext context)
    {
        CharacterController charaCollider = stateMachine.Player.Controller;
        Ray ray = new Ray();
        ray.origin = charaCollider.transform.position + (charaCollider.center * 1.4f) + charaCollider.transform.forward * (charaCollider.radius + .01f);
        ray.direction = charaCollider.transform.forward;
        Debug.DrawRay(ray.origin, ray.direction);
        if (!Physics.Raycast(ray, out RaycastHit hit, 1.6f)) return;

        if (hit.collider.TryGetComponent(out VillagerInterection villagerInterection))
        {
            villagerInterection.OpenStoreUI();
            //interactable.OpenUI(stateMachine);
            stateMachine.MovementInput = Vector2.zero;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        base.OnInteractStart(context);
    }
}
