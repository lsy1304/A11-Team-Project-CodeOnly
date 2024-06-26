using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public Weapon weapon;
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public PhysicsForce Force { get; private set; }

    [SerializeField] private HealthSystem healthSystem; // �ν����� â�� ǥ��
    [SerializeField] private CharacterStatHandler statHandler; // �ν����� â�� ǥ��

    private PlayerStateMachine stateMachine;
    public GameObject playerPrefab;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        Force = GetComponent<PhysicsForce>();

        stateMachine = new PlayerStateMachine(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void SendData()
    {
        DataManager.Instance.DataTransfer(Data, healthSystem, statHandler);
    }

    public CharacterStatHandler GetStatHandler()
    {
        return statHandler;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    public GameObject GetPlayerPrefab()
    {
        return playerPrefab;
    }

    public PlayerStateMachine GetPlayerStateMachine()
    {
        return stateMachine;
    }

    public float curHP
    {
        get { return healthSystem.CurHealth; }
        set { healthSystem.CurHealth = value; }
    }

    public void ResetPlayer()
    {
        // �÷��̾��� ���¸� �ʱ�ȭ
        Controller.enabled = false;
        Controller.enabled = true;
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
