using System.Collections;
using UnityEngine;

public class BattleEnemy : MonoBehaviour
{
    public HealthSystem healthSystem;
    public CharacterStatHandler statHandler;
    [SerializeField] private HealthBar healthBar; // HealthBar 스크립트 연결
    [SerializeField] private ParticleSystem hitEffect; // HitEffect 파티클 시스템 연결
    private AudioClip attackSound;
    private AudioClip skillSound;
    private AudioSource effectSource;

    public bool IsTakingTurn { get; private set; }
    private int currentSkillIndex = 0; // 현재 사용 중인 스킬 인덱스
    private int skillCooldown = 3; // 스킬 쿨다운 (임시로 3턴)
    private int skillCooldownCounter = 0; // 스킬 쿨다운 카운터

    private Animator animator;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<CharacterStatHandler>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        attackSound = BattleSoundManager.Instance.enemyAttackSound;
        skillSound = BattleSoundManager.Instance.enemySkillSound;
        effectSource = BattleSoundManager.Instance.effectSource;
        InitializeParticleSystems(); // 파티클 시스템 초기화
        animator.SetBool("isDead", false);
    }

    private void InitializeParticleSystems()
    {
        if (hitEffect == null)
        {
            hitEffect = transform.Find("EnemyHitEffect").GetComponent<ParticleSystem>();
            if (hitEffect == null)
            {
                Debug.LogError("EnemyHitEffect 파티클 시스템을 찾을 수 없습니다.");
            }
        }
    }

    public void Setup(HealthSystem enemyHealthSystem, CharacterStatHandler enemyStatHandler)
    {
        if (healthSystem == null || statHandler == null)
        {
            Debug.LogError("HealthSystem or CharacterStatHandler is not assigned.");
            return;
        }

        // 적 데이터 설정
        healthSystem.CurHealth = enemyHealthSystem.CurHealth;
        statHandler.CurrentStat = enemyStatHandler.CurrentStat.DeepCopy();

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
        StartCoroutine(TakeTurnRoutine());
    }

    private IEnumerator TakeTurnRoutine()
    {
        IsTakingTurn = true;
        Debug.Log("Enemy's turn started.");

        // 적의 턴 진행 로직 (예: 스킬 사용, 기본 공격 등)
        yield return new WaitForSeconds(1.0f); // 임시 대기 시간

        if (skillCooldownCounter == 0)
        {
            UseSkill(currentSkillIndex);
            skillCooldownCounter = skillCooldown; // 스킬 사용 후 쿨다운 초기화
        }
        else
        {
            BasicAttack();
            if (skillCooldownCounter > 0)
            {
                skillCooldownCounter--; // 쿨다운 카운터 감소
            }
        }

        // 추가: 적의 턴이 끝난 후 바로 종료
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        IsTakingTurn = false;
        Debug.Log("Enemy's turn ended.");
    }

    private void BasicAttack()
    {
        Debug.Log("Enemy attacks!");
        BattleSceneManager.Instance.actionText.text = "Enemy - Base Attack";
        animator.SetBool("isAttacking", true); // 기본 공격 애니메이션 시작
    }

    private void UseSkill(int skillIndex)
    {
        Debug.Log("Enemy uses skill!");
        BattleSceneManager.Instance.actionText.text = $"Enemy - {DataManager.Instance.EnemySkillData.SkillData[skillIndex].Name}";
        animator.SetBool("isUsingSkill", true); // 스킬 애니메이션 시작
    }

    public void ResetAttackTrigger()
    {
        animator.SetBool("isAttacking", false);
    }

    public void ResetSkillTrigger()
    {
        animator.SetBool("isUsingSkill", false);
    }

    public void PerformAttack()
    {
        // 애니메이션 이벤트에서 호출되는 공격 로직
        BattleSceneManager.Instance.battlePlayer.TakeDamage(statHandler.CurrentStat.BaseDamage);
        EndTurn();
    }

    public void PerformSkill()
    {
        // 애니메이션 이벤트에서 호출되는 스킬 로직
        int skillDamage = DataManager.Instance.EnemySkillData.SkillData[currentSkillIndex].Damage;
        int totalDamage = statHandler.CurrentStat.BaseDamage * skillDamage;
        Debug.Log($"Skill damage: {skillDamage}, Total damage: {totalDamage}");
        BattleSceneManager.Instance.battlePlayer.TakeDamage(totalDamage);
        EndTurn();
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        Debug.Log($"Enemy took {damage} damage, remaining health: {healthSystem.CurHealth}");
    }

    public void PlayHitEffect()
    {
        InitializeParticleSystems(); // 파티클 시스템 초기화
        if (hitEffect != null)
        {
            hitEffect.transform.position = BattleSceneManager.Instance.battlePlayer.transform.position + Vector3.up;
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

    public void EndTurn()
    {
        IsTakingTurn = false;
        Debug.Log("Enemy's turn ended.");
    }

    public void PlayEnemySkillSound()
    {
        effectSource.PlayOneShot(skillSound);
    }

    public void PlayEnemyAttackSound()
    {
        effectSource.PlayOneShot(attackSound);
    }

    public float Health => healthSystem.CurHealth;
}
