using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFill;

    public void SetHealth(float healthPercent)
    {
        healthFill.fillAmount = healthPercent;
    }
}
