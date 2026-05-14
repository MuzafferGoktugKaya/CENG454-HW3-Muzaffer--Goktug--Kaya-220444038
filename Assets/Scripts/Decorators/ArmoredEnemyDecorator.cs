using UnityEngine;

public class ArmoredEnemyDecorator : EnemyDecorator
{
    private int bonusHealth = 50;

    public ArmoredEnemyDecorator(IEnemy enemy) : base(enemy) { }

    public override int GetHealth()
    {
        return base.GetHealth() + bonusHealth;
    }

    public override void Initialize(Transform target)
    {
        base.Initialize(target);
        
        Debug.Log("<color=blue>Armored Enemy Spawned! Extra Health Applied.</color>");
    }
}