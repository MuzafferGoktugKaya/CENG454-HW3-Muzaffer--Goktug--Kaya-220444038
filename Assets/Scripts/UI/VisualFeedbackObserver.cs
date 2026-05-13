using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisualFeedbackObserver : MonoBehaviour
{
    [SerializeField] private Image damageImage;
    [SerializeField] private float flashSpeed = 5f;
    [SerializeField] private Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField] private float criticalThreshold = 0.3f; // sürekli flaşlamak için

    private float lastHealth = 1f;
    private bool isCritical = false;
    private Coroutine criticalFlashRoutine;

    private void OnEnable() => CoreController.OnCoreHealthChanged += HandleHealthChange;
    private void OnDisable() => CoreController.OnCoreHealthChanged -= HandleHealthChange;

    private void HandleHealthChange(float healthPercent)
    {
        if (damageImage == null) return;

        // one time flash
        if (healthPercent < lastHealth)
        {
            StartCoroutine(SingleFlashRoutine());
        }

        if (healthPercent <= criticalThreshold && !isCritical)
        {
            isCritical = true;
            criticalFlashRoutine = StartCoroutine(ContinuousFlashRoutine());
        }
        else if (healthPercent > criticalThreshold && isCritical)
        {
            isCritical = false;
            if (criticalFlashRoutine != null) StopCoroutine(criticalFlashRoutine);
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        }

        lastHealth = healthPercent;
    }

    IEnumerator SingleFlashRoutine()
    {
        float alpha = flashColor.a;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * flashSpeed;
            damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, Mathf.Max(alpha, 0));
            yield return null;
        }
    }

    IEnumerator ContinuousFlashRoutine()
    {
        while (isCritical)
        {
            // Parlat
            float alpha = 0;
            while (alpha < flashColor.a)
            {
                alpha += Time.deltaTime * flashSpeed;
                damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
                yield return null;
            }
            // Söndür
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * flashSpeed;
                damageImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f); // Flaşlar arası bekleme
        }
    }
}