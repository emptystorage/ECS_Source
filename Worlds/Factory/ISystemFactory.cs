namespace LETO.ECS
{
    public interface ISystemFactory
    {
        T Create<T>() where T : ECSSystem;
    }
}
