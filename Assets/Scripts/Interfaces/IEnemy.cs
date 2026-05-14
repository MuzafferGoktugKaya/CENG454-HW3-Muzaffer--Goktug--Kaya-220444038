using UnityEngine;

public interface IEnemy
{
    void Initialize(Transform target);
    float GetSpeed();
    int GetHealth();
    void Attack();
    void Die();
}