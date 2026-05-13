using UnityEngine;

public class ShootingAttackStrategy : IAttackStrategy
{
    public void Attack(Transform attacker, Transform target, GenericObjectPool pool)
    {
        // Havuzdan bir mermi al
        GameObject bulletObj = pool.GetFromPool(attacker.position, attacker.rotation);
        
        // Mermiyi ileriye doğru fırlat
        IProjectile projectile = bulletObj.GetComponent<IProjectile>();
        if (projectile != null)
        {
            projectile.Launch(15f, 5f, pool);
        }
    }
}