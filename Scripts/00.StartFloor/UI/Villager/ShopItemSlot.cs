using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    public ItemDiscription itemDiscription;
    public ItemSO itemSO;
    public Image icon;

    [Header("Text")]
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI priceTxt;


    public void SetSlot(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        icon.sprite = itemSO.icon;
        itemName.text = itemSO.itemName;
        priceTxt.text = $"Gold : {itemSO.price}";
    }

    public void OnClickItemSlot()
    {
        itemDiscription.ShowOption(itemSO);
    }
}
