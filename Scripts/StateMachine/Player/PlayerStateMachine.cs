using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player;
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public Transform MainCameraTrasnform { get; set; }
    public bool IsAttacking { get; set;}
    public bool IsInteracted { get; set;}
    public int ComboIndex { get; set;}


    public PlayerIdleState IdleState { get; private set;}
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }

    public PlayerAttackState AttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        MainCameraTrasnform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        AttackState = new PlayerAttackState(this);
    }
}
