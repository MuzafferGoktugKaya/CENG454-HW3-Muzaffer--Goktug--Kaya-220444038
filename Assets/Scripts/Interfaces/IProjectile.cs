public interface IProjectile
{
    void Launch(float speed, float damage, GenericObjectPool pool);
    void Deactivate();
}