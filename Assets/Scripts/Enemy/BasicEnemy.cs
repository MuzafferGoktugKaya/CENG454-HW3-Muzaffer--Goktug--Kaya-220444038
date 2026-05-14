using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth; 

    [Header("References")]
    [SerializeField] private GenericObjectPool projectilePool;

    private Transform coreTarget;
    private float nextAttackTime;
    private IMovementStrategy movementStrategy;
    private IAttackStrategy attackStrategy;

    public float GetSpeed() => moveSpeed;
    public int GetHealth() => currentHealth;

    public void Initialize(Transform target)
    {
        coreTarget = target;
        currentHealth = maxHealth; 
        
        if (movementStrategy == null) movementStrategy = new MoveToCoreStrategy();
        if (attackStrategy == null) attackStrategy = new ShootingAttackStrategy();

        if (projectilePool == null)
        {
            GameObject poolObj = GameObject.Find("ProjectilePool");
            if (poolObj != null) projectilePool = poolObj.GetComponent<GenericObjectPool>();
        }
    }

    public void SetMovementStrategy(IMovementStrategy strategy) => movementStrategy = strategy;
    public void SetAttackStrategy(IAttackStrategy strategy) => attackStrategy = strategy;

    void Update()
    {
        if (coreTarget == null) return;
        movementStrategy?.Move(this.transform, coreTarget, moveSpeed);
        
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyCore"))
        {
            IDamageable core = other.GetComponent<IDamageable>();
            if (core != null)
            {
                core.TakeDamage(20f);
                Die();
            }
        }
    }

    public void Attack() => attackStrategy?.Attack(transform, coreTarget, projectilePool);

    // space bar logic çağıracak
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage! Remaining: " + currentHealth);
        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}