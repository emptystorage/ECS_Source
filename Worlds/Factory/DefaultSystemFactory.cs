using System;

namespace LETO.ECS
{
    public sealed class DefaultSystemFactory : ISystemFactory
    {
        public T Create<T>() where T : ECSSystem
        {
            return Activator.CreateInstance<T>();
        }
    }
}
