using UnityEngine;

public interface IEnemy
{
    void Initialize(Transform target);
    void Attack();
}