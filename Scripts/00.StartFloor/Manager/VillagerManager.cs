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
                return "[E키를 눌러 무기 상점보기]";
            case Villager.Portion:
                return "[E키를 눌러 포션 상점보기]";
            case Villager.Armor:
                return "[E키를 눌러 방어구 상점보기]";
            case Villager.Healer:
                player.curHP += 1000000000000000000000000000f;
                return "너의 체력이 회복되었다네, 이 나라의 미래를 위해 힘써주게 젊은이";
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
