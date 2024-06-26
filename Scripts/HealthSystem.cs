using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private CharacterStatHandler _statHandler;
    [SerializeField] private float curHealth; // ���� ü���� �ν����� â���� ǥ��

    public float MaxHealth => _statHandler.CurrentStat.MaxHP;

    public event Action<float> OnHealthChanged; // ü�� ���� �̺�Ʈ

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
            OnHealthChanged?.Invoke(curHealth); // ü�� ���� �� �̺�Ʈ ȣ��
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
        CurHealth = Mathf.Max(curHealth - damage, 0f); // CurHealth �Ӽ��� ����Ͽ� ü�� ����
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
