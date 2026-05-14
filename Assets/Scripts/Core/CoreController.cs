using UnityEngine;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class CoreController : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private MeshRenderer meshRenderer;
    private Color originalColor;

    public float Health => currentHealth;

    public static event Action<float> OnCoreHealthChanged;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            originalColor = meshRenderer.material.color;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        // makes health 100% at the start 
        OnCoreHealthChanged?.Invoke(1.0f);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        // for notifying observers
        OnCoreHealthChanged?.Invoke(currentHealth / maxHealth);

        // makes core pitch white
        StartCoroutine(HitEffectRoutine());

        if (currentHealth <= 0) Die();
    }

    void Update() 
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
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
        Time.timeScale = 0f;
    }
}