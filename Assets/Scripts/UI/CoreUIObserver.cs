using UnityEngine;
using UnityEngine.UI;

public class CoreUIObserver : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color healthyColor = Color.green;
    [SerializeField] private Color criticalColor = Color.red;

    private void Start()
{
    UpdateHealthUI(1.0f); 
}

    private void OnEnable()
    {
        CoreController.OnCoreHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        CoreController.OnCoreHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(float healthPercent)
    {
        if (healthBar != null)
        {
            healthBar.value = healthPercent;
            fillImage.color = Color.Lerp(criticalColor, healthyColor, healthPercent);
        }
    }
}