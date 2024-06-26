using TMPro;
using UnityEngine;

public class InvenPlayerStat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHp;
    [SerializeField] TextMeshProUGUI playerAtk;
    [SerializeField] TextMeshProUGUI playerDef;

    [SerializeField] Player player;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] CharacterStatHandler characterStatHandler;

    public void AddPlayer()
    {
        healthSystem = player.GetHealthSystem();
        characterStatHandler = player.GetStatHandler();
    }
    void Update()
    {
        if (player != null)
        {
            UpDateStart();
        }
    }
    void UpDateStart()
    {
        if (healthSystem == null && characterStatHandler == null)
        {
            AddPlayer();
            return;
        }
        playerHp.text = $"HP/MaxHP : {player.curHP}/{characterStatHandler.CurrentStat.MaxHP}";
        playerAtk.text = $"ATK : {characterStatHandler.CurrentStat.BaseDamage}";
        playerDef.text = $"DEF : {characterStatHandler.CurrentStat.Def}";
    }
}
