using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    public Stat CurrentStat { get;  set; }
    [SerializeField] private Stat _baseStat;

    private bool isPlayer;

    void Awake()
    {
        // �� ������Ʈ�� �÷��̾ ���� ������ Ȯ��
        isPlayer = GetComponent<Player>() != null;

        UpdateStat();

        // �÷��̾��� ��쿡�� �κ��丮 �̺�Ʈ�� ����
        if (isPlayer)
        {
            Inventory.Instance.onEquipChange += UpdateStat;
        }
    }

    public void UpdateStat()
    {
        CurrentStat = _baseStat.DeepCopy();

        if (isPlayer)
        {
            EquipAll();
        }
    }

    private void EquipAll()
    {
        if (Inventory.Instance.nowWeaponEquiped != null)
        {
            EquipStat(Inventory.Instance.nowWeaponEquiped);
        }

        foreach (var dic in Inventory.Instance.nowArmorEquiped)
        {
            if (dic.Value != null)
            {
                EquipStat(dic.Value);
            }
        }
    }

    private void EquipStat(ItemSO itemSO)
    {
        foreach (var i in itemSO.itemStats)
        {
            switch (i.stat)
            {
                case ItemStats.Atk:
                    CurrentStat.BaseDamage += i.value;
                    break;
                case ItemStats.Hp:
                    CurrentStat.MaxHP += i.value;
                    Inventory.Instance.player.curHP += i.value;
                    break;
                case ItemStats.Def:
                    CurrentStat.Def += i.value;
                    break;
            }
        }
    }
}
