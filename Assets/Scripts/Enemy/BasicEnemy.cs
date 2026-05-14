using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackInterval = 2f;
    
    [Header("References")]
    [SerializeField] private GenericObjectPool projectilePool;

    private Transform coreTarget;
    private float nextAttackTime;
    
    private IMovementStrategy movementStrategy;
    private IAttackStrategy attackStrategy;

    public void Initialize(Transform target)
    {
        coreTarget = target;
        
        if (movementStrategy == null) movementStrategy = new MoveToCoreStrategy();
        if (attackStrategy == null) attackStrategy = new ShootingAttackStrategy();

        // to prevent type mismatch in Unity itself
        if (projectilePool == null)
        {
            GameObject poolObj = GameObject.Find("ProjectilePool");
            if (poolObj != null)
            {
                projectilePool = poolObj.GetComponent<GenericObjectPool>();
            }
        }
    }

    public void SetMovementStrategy(IMovementStrategy strategy) => movementStrategy = strategy;
    public void SetAttackStrategy(IAttackStrategy strategy) => attackStrategy = strategy;

    void Update()
    {
        if (coreTarget == null) return;

        // MoveToCore or Zigzag
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

    public void Attack()
    {
        if (projectilePool != null)
        {
            attackStrategy?.Attack(transform, coreTarget, projectilePool);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}