using System.Collections;
using UnityEngine;

public class BattlePlayer : MonoBehaviour
{
    public HealthSystem healthSystem;
    public CharacterStatHandler statHandler;
    [SerializeField] private HealthBar healthBar; // HealthBar 스크립트 연결
    public SkillButton firstSkillButton; // 첫 번째 스킬 버튼
    [SerializeField] private ParticleSystem hitEffect; // HitEffect 파티클 시스템 연결

    private AudioClip attackSound;
    private AudioClip skillSound;
    private AudioSource effectSource;

    public bool IsTakingTurn { get; private set; }

    private Animator animator; // 애니메이터 추가

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        animator = GetComponentInChildren<Animator>(); // 애니메이터 초기화
        InitializeParticleSystems(); // 파티클 시스템 초기화

        attackSound = BattleSoundManager.Instance.playerAttackSound;
        skillSound = BattleSoundManager.Instance.playerSkillSound;
        effectSource = BattleSoundManager.Instance.effectSource;
        animator.SetBool("isDead", false);
    }

    private void InitializeParticleSystems()
    {
        if (hitEffect == null)
        {
            hitEffect = transform.Find("PlayerHitEffect").GetComponent<ParticleSystem>();
            if (hitEffect == null)
            {
                Debug.LogError("PlayerHitEffect 파티클 시스템을 찾을 수 없습니다.");
            }
        }
    }

    public void Setup(HealthSystem playerHealthSystem, CharacterStatHandler playerStatHandler)
    {
        if (healthSystem == null || statHandler == null)
        {
            Debug.LogError("HealthSystem or CharacterStatHandler is not assigned.");
            return;
        }

        // 플레이어 데이터 설정
        healthSystem.CurHealth = playerHealthSystem.CurHealth;
        statHandler.CurrentStat = playerStatHandler.CurrentStat.DeepCopy();
        transform.position = playerHealthSystem.transform.position;

        // 체력 바 업데이트 설정
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(healthSystem.CurHealth); // 초기 체력 설정
        }
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth / healthSystem.MaxHealth); // 현재 체력을 백분율로 변환하여 설정
        }
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateHealthBar;
        }
    }

    public void TakeTurn()
    {
        IsTakingTurn = true;
        Debug.Log("Player's turn started.");
        // AttackButton이 눌릴 때까지 대기
    }

    public void Attack()
    {
        // 기본 공격 로직 구현
        Debug.Log("Player attacks!");
        animator.SetBool("isAttacking", true); // 공격 애니메이션 시작
        StartCoroutine(ResetBoolAfterAnimation("isAttacking"));
    }

    public void UseFirstSkill()
    {
        if (firstSkillButton.IsOnCooldown())
        {
            Debug.Log("Skill is on cooldown.");
            return;
        }

        // 첫 번째 스킬 사용 로직 구현
        Debug.Log("Using first skill.");
        animator.SetBool("isUsingSkill", true); // 스킬 애니메이션 시작

        // 스킬 쿨타임 설정
        int skillCooldown = 3; // 예시로 3턴 쿨타임 설정
        firstSkillButton.SetCooldown(skillCooldown);
        StartCoroutine(ResetBoolAfterAnimation("isUsingSkill"));
    }

    private IEnumerator ResetBoolAfterAnimation(string boolName)
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool(boolName, false);
    }

    public void PerformAttack()
    {
        // 애니메이션 이벤트에서 호출되는 공격 로직
        BattleSceneManager.Instance.battleEnemy.TakeDamage(statHandler.CurrentStat.BaseDamage);
        EndTurn();
    }

    public void PerformSkill()
    {
        // 애니메이션 이벤트에서 호출되는 스킬 로직
        int skillDamage = DataManager.Instance.PlayerSkillData.SkillData[0].Damage;
        int totalDamage = statHandler.CurrentStat.BaseDamage * skillDamage;
        Debug.Log($"Skill damage: {skillDamage}, Total damage: {totalDamage}");
        BattleSceneManager.Instance.battleEnemy.TakeDamage(totalDamage);
        EndTurn();
    }

    public void EndTurn()
    {
        IsTakingTurn = false;
        Debug.Log("Player's turn ended.");
        if (healthSystem.CurHealth <= 0)
        {
            PlayDeadAnimation();
            BattleSceneManager.Instance.ShowEndUI(false);
        }
    }


    public void OnTurnEnd()
    {
        firstSkillButton.ReduceCooldown();
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        Debug.Log($"Player took {damage} damage, remaining health: {healthSystem.CurHealth}");
    }

    public void PlayHitEffect()
    {
        InitializeParticleSystems(); // 파티클 시스템 초기화
        if (hitEffect != null)
        {
            hitEffect.transform.position = BattleSceneManager.Instance.battleEnemy.transform.position + new Vector3(0, 1.5f, -0.5f);
            hitEffect.Play();
        }
    }

    public void PlayDeadAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }
    }

    public void PlayPlayerSkillSound()
    {
        effectSource.PlayOneShot(skillSound);
    }
    public void PlayPlayerAttackSound()
    {
        effectSource.PlayOneShot(attackSound);
    }

    public float Health => healthSystem.CurHealth;
}
