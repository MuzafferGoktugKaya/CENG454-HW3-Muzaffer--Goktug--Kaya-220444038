using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform entityTransform, Transform target, float speed);
}

public class MoveToCoreStrategy : IMovementStrategy
{
    public void Move(Transform entityTransform, Transform target, float speed)
    {
        if (target == null) return;
        entityTransform.position = Vector3.MoveTowards(entityTransform.position, target.position, speed * Time.deltaTime);
        entityTransform.LookAt(target);
    }
}

public class ZigZagStrategy : IMovementStrategy
{
    public void Move(Transform entityTransform, Transform target, float speed)
    {
        if (target == null) return;
        Vector3 direction = (target.position - entityTransform.position).normalized;
        Vector3 sideStep = Vector3.Cross(direction, Vector3.up) * Mathf.Sin(Time.time * 5f) * 2f;
        entityTransform.position += (direction + sideStep).normalized * speed * Time.deltaTime;
        entityTransform.LookAt(target);
    }
}