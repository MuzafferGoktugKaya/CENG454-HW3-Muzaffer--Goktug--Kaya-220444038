using UnityEngine;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    private Transform coreTarget;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float damage = 10f;

    public void Initialize(Transform target)
    {
        coreTarget = target;
    }

    void Update()
    {
        if (coreTarget == null) return;

        transform.position = Vector3.MoveTowards(transform.position, coreTarget.position, speed * Time.deltaTime);
    }

    public void Attack()
    {
        //Will be connected to observer
        Debug.Log("Enemy attacking the core!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Core"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject); // destroy it atm, but we will change this to pooling later
            }
        }
    }
}