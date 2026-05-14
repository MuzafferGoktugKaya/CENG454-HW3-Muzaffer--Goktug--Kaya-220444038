using UnityEngine;

public abstract class EnemyDecorator : IEnemy
{
    protected IEnemy decoratedEnemy;

    public EnemyDecorator(IEnemy enemy)
    {
        this.decoratedEnemy = enemy;
    }

    public virtual void Initialize(Transform target) => decoratedEnemy.Initialize(target);
    public virtual float GetSpeed() => decoratedEnemy.GetSpeed();
    public virtual int GetHealth() => decoratedEnemy.GetHealth();
    public virtual void Attack() => decoratedEnemy.Attack();

    public virtual void Die()
    {
        decoratedEnemy.Die();
    }
}