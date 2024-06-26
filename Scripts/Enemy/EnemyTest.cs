using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [field: SerializeField] public EnemySO Data;
    [SerializeField] public HealthSystem healthSystem; // �ν����� â�� ǥ��
    [SerializeField] public CharacterStatHandler statHandler; // �ν����� â�� ǥ��
    public GameObject enemyPrefab;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
    }

    // Update is called once per frame
    void Update()
    {

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
}
