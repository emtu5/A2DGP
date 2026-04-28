using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    [SerializeField] private HealthSystem targetHealth;

    [Header("Colors")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color lowHealthColor = Color.red;

    private void OnEnable()
    {
        targetHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        targetHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float current, float max)
    {
        float fill = current / max;

        healthFillImage.fillAmount = fill;
        healthFillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, fill);
    }
}