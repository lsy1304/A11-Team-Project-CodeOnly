using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponSlot : MonoBehaviour
{
    [SerializeField] Image slotColor;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemStat;
    [SerializeField] ItemSO itemSO;

    public void SetSlot(ItemSO itemSO)
    {
        Inventory.Instance.onEquipChange += SetSlotColor;
        this.itemSO = itemSO;
        icon.sprite = itemSO.icon;
        itemName.text = itemSO.itemName;
        itemStat.text = string.Empty;

        for (int i = 0; i < itemSO.itemStats.Length; i++)
        {
            ItemStat nowStat = itemSO.itemStats[i];

            if (nowStat.stat == ItemStats.Level )
            {
                continue;
            }
            if (i != 0)
            {
                itemStat.text += "\n";
            }
            itemStat.text += $"{nowStat.stat} : {nowStat.value}";
        }
        SetSlotColor();
    }

    private void SetSlotColor()
    {
        if (Inventory.Instance.isEquip(itemSO))
        {
            slotColor.color = new Color(77 / 255f, 17 / 255f, 16 / 255f);
        }
        else
        {
            slotColor.color = new Color(2 / 255f, 1 / 255f, 31 / 255f);
        }
    }

    public void Equipment()
    {
        if (Inventory.Instance.isEquip(itemSO))
        {
            if (itemSO.itemType == ItemType.Weapon)
            {
                Inventory.Instance.UnWeaponEquip();
            }
            else
            {
                Inventory.Instance.UnArmorEquiped(itemSO.armorType);
            }
        }
        else
        {
            Inventory.Instance.Equip(itemSO);
        }
    }
}
