using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler _statHandler;
    [SerializeField] private float curHealth; // 현재 체력을 인스펙터 창에서 표시

    public float MaxHealth => _statHandler.CurrentStat.MaxHP;

    public event Action<float> OnHealthChanged; // 체력 변경 이벤트

    public float CurHealth
    {
        get
        {
            //if (curHealth >= MaxHealth)
            //{
            //    curHealth = MaxHealth;
            //}
            return curHealth;
        }
        set
        {
            curHealth = value;
            //if (curHealth >= MaxHealth)
            //{
            //    curHealth = MaxHealth;
            //}
            OnHealthChanged?.Invoke(curHealth); // 체력 변경 시 이벤트 호출
        }
    }

    public event Action OnDie;
    public event Action OnHeal;

    private void Awake()
    {
        _statHandler = GetComponent<CharacterStatHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurHealth = Mathf.Max(curHealth - damage, 0f); // CurHealth 속성을 사용하여 체력 변경
        if (curHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

    public void Heal(ItemSO item) // StatType == Heal && ItemType == Potion
    {
        curHealth = Mathf.Min(curHealth + item.itemStats[0].value, MaxHealth);
        OnHeal?.Invoke();
    }
}
