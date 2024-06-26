using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [field: Header("Reference")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    private EnemyStateMachine stateMachine;

    public ForceReceiver ForceReceiver { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Transform Target; // 목표 위치 (플레이어 등)

    [SerializeField] public HealthSystem healthSystem;
    [SerializeField] public CharacterStatHandler statHandler;
    public GameObject enemyPrefab;
    private NavMeshAgent agent;
    public float detectDistance;

    public float updateInterval = 3f; // updateInterval 변수를 선언하고 초기화합니다.

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.ChangeState(stateMachine.IdleState);

        NavMeshAgent = GetComponent<NavMeshAgent>();

        NavMeshAgent.speed = 3f; // 이동 속도 설정
        NavMeshAgent.acceleration = 8f; // 가속도 설정

        NavMeshAgent.speed = Data.MoveSpeed; // 적 캐릭터의 이동 속도 설정
        NavMeshAgent.acceleration = Data.Acceleration; // 적 캐릭터의 가속도 설정

        StartCoroutine(MoveRoutine()); // 이동 코루틴 시작
    }

    private void OnEnable()
    {
        // 적이 활성화될 때 NavMeshAgent 경로 재설정
        if (NavMeshAgent != null)
        {
            NavMeshAgent.isStopped = false; // 에이전트를 멈추지 않도록 설정
            StartCoroutine(MoveRoutine()); // 이동 코루틴 다시 시작
        }
    }

    private void OnDisable()
    {
        // 적이 비활성화될 때 NavMeshAgent 정지
        if (NavMeshAgent != null)
        {
            NavMeshAgent.isStopped = true;
            StopCoroutine(MoveRoutine()); // 이동 코루틴 중지
        }
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            NavMeshAgent.SetDestination(GetRandomPositionOnNavMesh());
            yield return new WaitForSeconds(updateInterval); // 일정 시간 동안 기다립니다.
        }
    }

    public Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f; // 원하는 범위 내의 랜덤한 방향 벡터를 생성합니다.
        randomDirection += transform.position; // 랜덤 방향 벡터를 현재 위치에 더합니다.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
        {
            return hit.position; // NavMesh 위의 랜덤 위치를 반환합니다.
        }
        else
        {
            return transform.position; // NavMesh 위의 랜덤 위치를 찾지 못한 경우 현재 위치를 반환합니다.
        }
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

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Player player)) // last attack
            {
                player.SendData();
                GetComponentInParent<Enemy>().SendData();
                BattleManager.Instance.StartBattle(player, this, true);
            }
        }
    }
}
