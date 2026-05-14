using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IProjectile
{
    private float projectileSpeed = 0f;
    private float projectileDamage;
    private GenericObjectPool myPool;

    public void Launch(float speed, float damage, GenericObjectPool pool)
    {
        projectileSpeed = speed;
        projectileDamage = damage;
        myPool = pool;

        gameObject.SetActive(true); // to prevent bullets from spawning inside the core

        CancelInvoke();
        Invoke(nameof(Deactivate), 3f);
    }

    void Update()
    {
        if (projectileSpeed > 0)
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    public void Deactivate()
    {
        projectileSpeed = 0f;
        
        if (myPool != null)
        {
            myPool.ReturnToPool(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnergyCore"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();
            if (hit != null)
            {
                hit.TakeDamage(projectileDamage);
                Deactivate();
            }
        }
    }
}