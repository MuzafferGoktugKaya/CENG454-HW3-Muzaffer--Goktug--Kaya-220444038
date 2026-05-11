using UnityEngine;
using System;

public class CoreController : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float Health => currentHealth;

    // Observer Pattern için Event
    public static event Action<float> OnCoreHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        OnCoreHealthChanged?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        Debug.Log("Core Breached! Game Over.");
        // Buraya patlama efekti veya sahne resetleme eklemeliyim
    }
}