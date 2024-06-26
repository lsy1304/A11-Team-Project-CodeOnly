using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;
    [SerializeField] List<ItemSO> items;
    [SerializeField] List<ShopItemSlot> shopItemSlots;
    [SerializeField] GameObject shopItemSlot;
    [SerializeField] Transform content;
    [SerializeField] ItemDiscription itemDiscription;
    [SerializeField] TextMeshProUGUI playerGold;

    Villager nowVillager;
    public void OnShop(Villager villager)
    {
        shopMenu.SetActive(true);
        ShopSetting(villager);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShopSetting(Villager villager)
    {
        playerGold.text = $"Gold : {Inventory.Instance.playerGold.Gold.ToString()}";
        nowVillager = villager;
        itemDiscription.ClearTxt();
        if (items.Count == 0 )
        {
            return;
        }
        if (shopItemSlots.Count > 0)
        {
            ClearShop();
        }
        shopItemSlots = new List<ShopItemSlot>();
        foreach (var item in items)
        {
            switch (villager)
            {
                case Villager.Weapon:
                    if (item.itemType == ItemType.Weapon)
                    {
                        ShopSetting(item);
                    }
                    break;
                case Villager.Portion:
                    if (item.itemType == ItemType.Portion)
                    {
                        ShopSetting(item);
                    }
                    break;
                case Villager.Armor:
                    if (item.itemType == ItemType.Armor)
                    {
                        ShopSetting(item);
                    }
                    break;
            }
        }
    }
    void ShopSetting(ItemSO item)
    {
        ShopItemSlot slot = Instantiate(shopItemSlot, content).GetComponent<ShopItemSlot>();
        shopItemSlots.Add(slot);
        slot.SetSlot(item);
        slot.itemDiscription = itemDiscription;
    }
    void ClearShop()
    {
        foreach(Transform con in content.transform)
        {
            Destroy(con.gameObject);
        }

        shopItemSlots.Clear();
    }
    public void BuyItem(ItemSO itemSO)
    {
        if (Inventory.Instance.playerGold.Gold >= itemSO.price)
        {
            Inventory.Instance.playerGold.Gold -= itemSO.price;
            playerGold.text = $"Gold : {Inventory.Instance.playerGold.Gold.ToString()}";
            foreach (var item in items)
            {
                if (item == itemSO)
                {
                    Inventory.Instance.AddItem(itemSO);
                    if (item.itemType != ItemType.Portion)
                    {
                        items.Remove(item);
                    }
                    ShopSetting(nowVillager);
                    return;
                }
            }
        }
    }
}
