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
        // Observer Kaydı: Mesajı dinlemeye başlaması için
        CoreController.OnCoreHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        // Temizlik: Hafıza sızıntısını önlemek için
        CoreController.OnCoreHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(float healthPercent)
    {
        if (healthBar != null)
        {
            healthBar.value = healthPercent;
            // Görsel efekt: Can azaldıkça bar kırmızıya döner
            fillImage.color = Color.Lerp(criticalColor, healthyColor, healthPercent);
        }
    }
}