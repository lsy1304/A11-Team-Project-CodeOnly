using UnityEngine;

public class MainSceneManager : Singleton<MainSceneManager>
{
    public Player player;
    public Enemy[] enemies;
    public Enemy currentEnemy;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);  // 씬이 바뀌어도 파괴되지 않도록 설정
        FindPlayerAndEnemies();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        FindPlayerAndEnemies();
    }

    private void FindPlayerAndEnemies()
    {
        player = FindObjectOfType<Player>(true);  // 비활성화된 오브젝트도 찾기 위해 true를 추가
        enemies = FindObjectsOfType<Enemy>(true); // 모든 적을 찾기 위해 true를 추가

        if (player == null)
        {
            Debug.LogError("Player not found in MainScene.");
        }

        if (enemies.Length == 0)
        {
            Debug.LogError("Enemies not found in MainScene.");
        }
    }

    public void PlayerWins(BattlePlayer battlePlayer, BattleEnemy battleEnemy)
    {
        FindPlayerAndEnemies();

        if (player == null)
        {
            Debug.LogError("Player not found in MainScene.");
            return;
        }

        // 플레이어 데이터 갱신
        player.curHP = battlePlayer.Health; // 전투 후 현재 체력 반영
        player.GetStatHandler().CurrentStat = battlePlayer.statHandler.CurrentStat.DeepCopy();
        player.gameObject.SetActive(true);

        currentEnemy.healthSystem.CurHealth = battleEnemy.Health;

        // 패배한 적 비활성화
        if (currentEnemy != null && currentEnemy.healthSystem.CurHealth <= 0)
        {
            Destroy(currentEnemy.gameObject);
            DungeonManager.Instance.randomManager.decorator.enemyCount--;
        }
        currentEnemy = null;

        // 커서 설정 복원
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ResetScene();
    }

    public void GameOver()
    {
        // 게임 오버 처리
        Debug.Log("Game Over");
        // 추가적인 게임 오버 로직 구현
        Application.Quit();

        // 커서 설정 복원
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 플레이어를 던전 입구로 이동
        GameObject dungeonEntrance = GameObject.Find("DungeonEntrance");
        player.curHP = 0;
        if (dungeonEntrance != null)
        {
            player.transform.position = dungeonEntrance.transform.position;
        }
        else
        {
            Debug.LogError("DungeonEntrance not found in MainScene.");
        }
        ResetScene();
    }

    public void ResetScene()
    {
        FindPlayerAndEnemies();

        // 플레이어를 다시 활성화
        if (player != null)
        {
            player.gameObject.SetActive(true);
            player.ResetPlayer(); // 플레이어 초기화
        }
        else
        {
            Debug.LogError("Player not found when resetting scene.");
        }

        // 모든 적을 다시 활성화
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(true);
            }
        }
        Debug.Log(DungeonManager.Instance.randomManager.decorator.enemyCount);
        if (DungeonManager.Instance.randomManager.decorator.enemyCount <= 0)
        {
            StartCoroutine(DungeonManager.Instance.ReturnStartPalce(player));
        }
    }

    public void SetCurrentEnemy(Enemy enemy)
    {
        currentEnemy = enemy;
    }
}
