using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IProjectile
{
    private float projectileSpeed;
    private float projectileDamage;
    private GenericObjectPool myPool;

    public void Launch(float speed, float damage, GenericObjectPool pool)
    {
        projectileSpeed = speed;
        projectileDamage = damage;
        myPool = pool;

        // 3 saniye sonra çarpmasa bile havuza dönsün (Hafıza yönetimi)
        CancelInvoke();
        Invoke(nameof(Deactivate), 3f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    public void Deactivate()
    {
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
        // IDamageable arayüzüne sahip herhangi bir şeye çarparsa hasar ver
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            hit.TakeDamage(projectileDamage);
            Deactivate();
        }
    }
}