using TMPro;
using UnityEngine;

public class ItemDiscription : MonoBehaviour
{
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI statTxt;
    public TextMeshProUGUI discriptionTxt;
    public TextMeshProUGUI discriptionNameTxt;

    public ItemSO item;

    public void ShowOption(ItemSO itemSO)
    {
        item = itemSO;
        discriptionTxt.text = $"\"{itemSO.itemDescription}\"";
        discriptionNameTxt.text = itemSO.itemName;

        statTxt.text = string.Empty;
        for (int i = 0; i<itemSO.itemStats.Length; i++)
        {
            ItemStat nowStat = itemSO.itemStats[i];
            if (i != 0)
            {
                statTxt.text += "\n";
            }
            statTxt.text += $"{nowStat.stat} : {nowStat.value}";
        }
        levelTxt.text = $"Level : {ItemStats.Level}";
    }

    public void BuyItem()
    {
        VillagerManager.Instance.shopManager.BuyItem(item);
    }

    public void ClearTxt()
    {
        levelTxt.text = string.Empty ;
        discriptionTxt.text = string.Empty ;
        discriptionNameTxt.text = string.Empty ;
        statTxt.text = string.Empty ;
    }
}
