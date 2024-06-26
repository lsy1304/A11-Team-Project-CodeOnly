using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItemSlot : MonoBehaviour
{
    public ItemSO itemSO;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI valueTxt;
    public int stack;

    public void SetSlot(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        icon.sprite = itemSO.icon;
        if (itemSO != null )
        {
            stack++;
            valueTxt.text = stack.ToString();
        }
        else
        {
            valueTxt.text =string.Empty;
        }
    }

    public void UseItem()
    {
        if (itemSO == null)
        {
            return;
        }
        stack--;
        HealthSystem hp = Inventory.Instance.player.GetHealthSystem();
        hp.Heal(itemSO);
        valueTxt.text = stack.ToString();
        if (stack <= 0)
        {
            itemSO = null;
            icon.sprite = null;
            valueTxt.text = string.Empty;
        }
    }
}
