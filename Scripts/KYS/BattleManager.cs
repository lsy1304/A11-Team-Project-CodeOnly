using System.Collections;
using UnityEngine.SceneManagement;

public class BattleManager : Singleton<BattleManager>
{
    private Player mainScenePlayer;
    private Enemy mainSceneEnemy;
    private bool enemyFirstTurn;

    public void StartBattle(Player player, Enemy enemy, bool isEnemyFirstTurn)
    {
        // MainScene의 Player와 Enemy 비활성화
        mainScenePlayer = player;
        mainSceneEnemy = enemy;
        enemyFirstTurn = isEnemyFirstTurn;

        mainScenePlayer.gameObject.SetActive(false);
        mainSceneEnemy.gameObject.SetActive(false);

        // 모든 적 비활성화
        Enemy[] allEnemies = FindObjectsOfType<Enemy>(true);
        foreach (var e in allEnemies)
        {
            e.gameObject.SetActive(false);
        }

        // 현재 충돌한 적 설정
        MainSceneManager.Instance.SetCurrentEnemy(mainSceneEnemy);

        // BattleScene 로드
        StartCoroutine(LoadBattleScene(mainScenePlayer, mainSceneEnemy));
    }

    private IEnumerator LoadBattleScene(Player player, Enemy enemy)
    {
        SceneManagement.Instance.LoadScene("BattleScene");

        // 씬 로드가 완료될 때까지 대기
        while (SceneManagement.Instance.IsSceneLoading())
        {
            yield return null;
        }

        // BattleScene 로드 완료 후 초기화
        BattleSceneManager battleSceneManager = FindObjectOfType<BattleSceneManager>();
        battleSceneManager.Initialize(player, enemy, enemyFirstTurn);
    }

    public void EndBattle(bool playerWon)
    {
        // BattleScene 언로드
        StartCoroutine(UnloadBattleScene(playerWon));
    }

    private IEnumerator UnloadBattleScene(bool playerWon)
    {
        SceneManagement.Instance.UnloadScene("BattleScene");

        // 씬 언로드가 완료될 때까지 대기
        while (SceneManagement.Instance.IsSceneLoading())
        {
            yield return null;
        }

        // MainScene으로 돌아가기
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("NewMap"));

        if (playerWon)
        {
            // 플레이어가 승리했을 때
            MainSceneManager.Instance.PlayerWins(BattleSceneManager.Instance.battlePlayer, BattleSceneManager.Instance.battleEnemy);
        }
        else
        {
            // 플레이어가 패배했을 때
            MainSceneManager.Instance.GameOver();
        }

        // 전투가 끝난 후 플레이어와 적을 초기화
        mainScenePlayer.gameObject.SetActive(true);
        mainScenePlayer.ResetPlayer();
    }
}
