using UnityEngine;
public interface IAttackStrategy
{
    void Attack(Transform attacker, Transform target, GenericObjectPool pool);
}