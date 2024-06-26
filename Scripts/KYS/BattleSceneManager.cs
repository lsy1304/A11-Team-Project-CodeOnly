using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneManager : Singleton<BattleSceneManager>
{
    public Button attackButton; // AttackButton UI 연결
    public SkillButton firstSkillButton; // 첫 번째 스킬 버튼
    public Camera battleCamera; // BattleScene에 배치된 카메라
    public TextMeshProUGUI turnText; // TurnText UI 연결
    public TextMeshProUGUI actionText; // ActionText UI 연결

    public Button pauseButton; // PauseButton UI 연결
    public GameObject pauseMenu; // PauseMenu UI 연결
    public Button resumeButton; // ResumeButton UI 연결
    public Button giveUpButton; // GiveUpButton UI 연결

    public GameObject victoryUI; // 승리 UI 연결
    public GameObject defeatUI; // 패배 UI 연결
    public Button victoryNextButton; // 승리 UI의 다음 버튼 연결
    public Button defeatNextButton; // 패배 UI의 다음 버튼 연결

    public BattlePlayer battlePlayer; // BattleScene에 배치된 Player 오브젝트
    public BattleEnemy battleEnemy; // BattleScene에 배치된 Enemy 오브젝트

    public ParticleSystem playerHitEffect; // 플레이어가 공격받을 때 파티클
    public ParticleSystem enemyHitEffect; // 적이 공격받을 때 파티클

    private bool isPlayerTurn;
    private bool battleEnded;
    private bool isPaused;

    private Vector3 enemyOffset = new Vector3(0f, 0f, 5f); // 적의 오프셋
    private Vector3 cameraOffset = new Vector3(2f, 1f, -1f); // 카메라 오프셋

    private int turnCounter; // 턴 카운터

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            InitializeComponents();
            pauseMenu.SetActive(false);
            BattleSoundManager.Instance.TransitionToBattleBGM();
        }
    }

    private void InitializeComponents()
    {
        attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        firstSkillButton = GameObject.Find("FirstSkillButton").GetComponent<SkillButton>();
        battleCamera = GameObject.Find("BattleCamera").GetComponent<Camera>();
        turnText = GameObject.Find("TurnText").GetComponentInChildren<TextMeshProUGUI>();
        actionText = GameObject.Find("ActionText").GetComponentInChildren<TextMeshProUGUI>();

        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseMenu = GameObject.Find("PauseMenu");
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        giveUpButton = GameObject.Find("GiveUpButton").GetComponent<Button>();

        victoryUI = GameObject.Find("VictoryUI");
        defeatUI = GameObject.Find("DefeatUI");
        victoryNextButton = GameObject.Find("VictoryNextButton").GetComponent<Button>();
        defeatNextButton = GameObject.Find("DefeatNextButton").GetComponent<Button>();

        // UI 초기 상태
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);

        // 버튼 클릭 이벤트 등록
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackButtonClicked);
            attackButton.interactable = false; // 초기에는 비활성화
        }

        if (firstSkillButton != null)
        {
            firstSkillButton.button.onClick.AddListener(OnFirstSkillButtonClicked);
            firstSkillButton.button.interactable = false; // 초기에는 비활성화
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
        }

        if (giveUpButton != null)
        {
            giveUpButton.onClick.AddListener(OnGiveUpButtonClicked);
        }

        if (victoryNextButton != null)
        {
            victoryNextButton.onClick.AddListener(OnVictoryNextButtonClicked);
        }

        if (defeatNextButton != null)
        {
            defeatNextButton.onClick.AddListener(OnDefeatNextButtonClicked);
        }

        // 초기에는 일시정지 메뉴를 비활성화
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // 파티클 시스템 연결
        playerHitEffect = GameObject.Find("PlayerHitEffect").GetComponent<ParticleSystem>();
        enemyHitEffect = GameObject.Find("EnemyHitEffect").GetComponent<ParticleSystem>();
    }

    public void Initialize(Player player, Enemy enemy, bool isEnemyFirstTurn)
    {
        // BattleScene에서 BattlePlayer와 BattleEnemy 오브젝트 찾기
        battlePlayer = FindObjectOfType<BattlePlayer>();
        battleEnemy = FindObjectOfType<BattleEnemy>();

        if (battlePlayer == null || battleEnemy == null)
        {
            Debug.LogError("BattlePlayer or BattleEnemy not found in BattleScene.");
            return;
        }

        // MainScene의 Player와 Enemy 데이터를 BattleScene의 오브젝트에 설정
        battlePlayer.Setup(player.GetHealthSystem(), player.GetStatHandler());
        battleEnemy.Setup(enemy.GetHealthSystem(), enemy.GetStatHandler());

        // 적의 위치 설정
        SetEnemyPosition();

        // 카메라 위치 설정
        PositionCamera();

        // 커서 설정
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // 선턴 설정
        isPlayerTurn = !isEnemyFirstTurn;

        StartBattle();
    }

    private void SetEnemyPosition()
    {
        if (battlePlayer != null && battleEnemy != null)
        {
            battleEnemy.transform.position = battlePlayer.transform.position + enemyOffset;
        }
    }

    private void PositionCamera()
    {
        if (battlePlayer != null && battleEnemy != null && battleCamera != null)
        {
            // 카메라 위치 설정
            battleCamera.transform.position = battlePlayer.transform.position + cameraOffset;

            // 플레이어와 적의 중간 지점을 바라보도록 설정
            Vector3 midPoint = (battlePlayer.transform.position + battleEnemy.transform.position) / 2;
            battleCamera.transform.LookAt(midPoint + Vector3.up);
        }
    }

    private void StartBattle()
    {
        turnCounter = 0; // 턴 카운터 초기화
        battleEnded = false;
        isPaused = false;
        UpdateTurnText(); // 초기 턴 텍스트 업데이트
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (!battleEnded)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            if (isPlayerTurn)
            {
                Debug.Log("Player's turn.");
                if (attackButton != null)
                {
                    attackButton.interactable = true; // 플레이어 턴일 때 버튼 활성화
                }
                if (firstSkillButton != null)
                {
                    firstSkillButton.button.interactable = true; // 플레이어 턴일 때 스킬 버튼 활성화
                }
                battlePlayer.TakeTurn();
                while (battlePlayer.IsTakingTurn)
                {
                    yield return null;
                }

                if (attackButton != null)
                {
                    attackButton.interactable = false; // 턴 종료 시 버튼 비활성화
                }
                if (firstSkillButton != null)
                {
                    firstSkillButton.button.interactable = false; // 턴 종료 시 스킬 버튼 비활성화
                }

                if (CheckBattleEnd())
                {
                    yield break; // 코루틴 종료
                }

                isPlayerTurn = false;
            }
            else
            {
                Debug.Log("Enemy's turn.");
                battleEnemy.TakeTurn();
                while (battleEnemy.IsTakingTurn)
                {
                    yield return null;
                }

                if (CheckBattleEnd())
                {
                    yield break; // 코루틴 종료
                }

                isPlayerTurn = true;
            }

            turnCounter++;
            if (turnCounter % 2 == 0) // 플레이어와 적의 턴이 모두 끝났을 때
            {
                battlePlayer.OnTurnEnd();
            }
            UpdateTurnText();
        }

    }

    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = $"Turn: {turnCounter / 2 + 1}";
        }
    }

    private bool CheckBattleEnd()
    {
        if (battlePlayer.Health <= 0 || battleEnemy.Health <= 0)
        {
            if (battlePlayer.Health <= 0)
            {
                Debug.Log("Player is dead.");
                battlePlayer.PlayDeadAnimation();
            }

            if (battleEnemy.Health <= 0)
            {
                Debug.Log("Enemy is dead.");
                battleEnemy.PlayDeadAnimation();
            }

            ShowEndUI(battlePlayer.Health > 0);
            BattleSoundManager.Instance.TransitionToMainBGM();
            battleEnded = true;
            return true;
        }
        return false;
    }

    public void ShowEndUI(bool playerWon)
    {
        if (playerWon)
        {
            victoryUI.SetActive(true);
        }
        else
        {
            defeatUI.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (attackButton != null)
        {
            attackButton.onClick.RemoveListener(OnAttackButtonClicked);
        }
        if (firstSkillButton != null)
        {
            firstSkillButton.button.onClick.RemoveListener(OnFirstSkillButtonClicked);
        }
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }
        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
        }
        if (giveUpButton != null)
        {
            giveUpButton.onClick.RemoveListener(OnGiveUpButtonClicked);
        }
        if (victoryNextButton != null)
        {
            victoryNextButton.onClick.RemoveListener(OnVictoryNextButtonClicked);
        }
        if (defeatNextButton != null)
        {
            defeatNextButton.onClick.RemoveListener(OnDefeatNextButtonClicked);
        }
    }

    private void OnAttackButtonClicked()
    {
        if (isPlayerTurn && battlePlayer.IsTakingTurn)
        {
            attackButton.interactable = false;
            battlePlayer.Attack();
            actionText.text = "Player - Base Attack";
        }
    }

    private void OnFirstSkillButtonClicked()
    {
        if (isPlayerTurn && battlePlayer.IsTakingTurn)
        {
            firstSkillButton.button.interactable = false;
            battlePlayer.UseFirstSkill();
            actionText.text = $"Player - {DataManager.Instance.PlayerSkillData.SkillData[0].Name}";
        }
    }

    private void OnPauseButtonClicked()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // 게임 시간을 멈춤
    }

    private void OnResumeButtonClicked()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // 게임 시간을 다시 진행
    }

    private void OnGiveUpButtonClicked()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // 게임 시간을 다시 진행

        // 플레이어의 체력을 절반으로 감소
        battlePlayer.healthSystem.CurHealth /= 2;

        // MainScene으로 돌아가기
        ShowEndUI(false);
    }

    private void OnVictoryNextButtonClicked()
    {
        BattleManager.Instance.EndBattle(true);
    }

    private void OnDefeatNextButtonClicked()
    {
        BattleManager.Instance.EndBattle(false);
    }
}
