using UnityEngine;

public enum Villager
{
    Weapon,
    Portion,
    Armor,
    Healer
}
public class VillagerManager : Singleton<VillagerManager>
{
    [HideInInspector] public ItemShopManager shopManager;
    public PlayerStateMachine playerState;
    public Player player;
    protected override void Awake()
    {
        base.Awake();

        shopManager = GetComponent<ItemShopManager>();
    }

    public string VillagerTxt(Villager name)
    {
        switch (name)
        {
            case Villager.Weapon:
                return "[EŰ�� ���� ���� ��������]";
            case Villager.Portion:
                return "[EŰ�� ���� ���� ��������]";
            case Villager.Armor:
                return "[EŰ�� ���� �� ��������]";
            case Villager.Healer:
                player.curHP += 1000000000000000000000000000f;
                return "���� ü���� ȸ���Ǿ��ٳ�, �� ������ �̷��� ���� �����ְ� ������";
        }
        return null;
    }

    public void VillagerOption(Villager villager)
    {
        shopManager.OnShop(villager);
    }

    public void CloseShop()
    {
        shopManager.CloseShop();
    }
}
