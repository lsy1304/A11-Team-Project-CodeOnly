using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI cooldownText;

    private int cooldownTurns;
    private int remainingTurns;

    private void Start()
    {
        button = GetComponent<Button>();
        cooldownText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateCooldownText();
    }

    public void SetCooldown(int turns)
    {
        cooldownTurns = turns;
        remainingTurns = turns;
        button.interactable = false;
        UpdateCooldownText();
    }

    public void ReduceCooldown()
    {
        if (remainingTurns > 0)
        {
            remainingTurns--;
            UpdateCooldownText();

            if (remainingTurns == 0)
            {
                button.interactable = true;
                UpdateCooldownText();
            }
        }
    }

    private void UpdateCooldownText()
    {
        if (remainingTurns > 0)
        {
            cooldownText.text = remainingTurns.ToString();
        }
        else
        {
            cooldownText.text = "Skill1"; // 스킬 사용 가능할 때 텍스트
        }
    }

    public bool IsOnCooldown()
    {
        return remainingTurns > 0;
    }
}
