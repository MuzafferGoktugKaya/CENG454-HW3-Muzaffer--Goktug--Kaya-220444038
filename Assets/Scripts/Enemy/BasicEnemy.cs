using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    private Transform coreTarget;
    private IAttackStrategy attackStrategy;
    
    [SerializeField] private GenericObjectPool projectilePool;
    [SerializeField] private float attackInterval = 2f;
    private float nextAttackTime;

    public void Initialize(Transform target)
    {
        coreTarget = target;
        attackStrategy = new ShootingAttackStrategy(); 
    }

    void Update()
    {
        if (coreTarget == null) return;

        // Çekirdeğe doğru ilerle
        transform.position = Vector3.MoveTowards(transform.position, coreTarget.position, 2f * Time.deltaTime);
        transform.LookAt(coreTarget); // Hedefe doğru bak

        // Mesafe kontrolü ve saldırı zamanlaması
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    public void Attack()
    {
        // Stratejiyi kullanarak ateş et!
attackStrategy?.Attack(transform, coreTarget, projectilePool);    }
}