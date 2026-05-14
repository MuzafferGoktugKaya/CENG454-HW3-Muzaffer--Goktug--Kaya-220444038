using UnityEngine;
using System;
using System.Collections;

public class CoreController : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public float Health => currentHealth; 

    private MeshRenderer meshRenderer;
    private Color originalColor;

    public static event Action<float> OnCoreHealthChanged;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null) originalColor = meshRenderer.material.color;
    }

    void Start()
    {
        currentHealth = maxHealth;
        OnCoreHealthChanged?.Invoke(1.0f);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        // UI Gözlemcilerini bilgilendir
        OnCoreHealthChanged?.Invoke(currentHealth / maxHealth);
        
        StartCoroutine(HitEffectRoutine());

        if (currentHealth <= 0) Die();
    }

    private IEnumerator HitEffectRoutine()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.white; 
            yield return new WaitForSeconds(0.1f);
            meshRenderer.material.color = originalColor;
        }
    }

    public void Die()
    {
        Debug.Log("Core Breached! Game Over.");
        GameManager.Instance.GameOver(); 
    }
}