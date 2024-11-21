using System;

namespace LETO.ECS
{
    public enum WriteType
    {
        Write,
        ReWrite
    }

    public static class EntityExtension
    {
        public static void Destroy(this Entity entity)
        {
            entity.MyWorld.World.DestroyEntity(entity);
        }

        public static Entity AddComponent<T>(this Entity entity, WriteType writeType = WriteType.Write)
            where T : struct, IComponent
        {
            var component = Activator.CreateInstance<T>();
            entity.AddComponent(ref component, writeType);

            return entity;
        }

        public static Entity AddComponent<T>(this Entity entity, ref T component, WriteType writeType = WriteType.Write)
            where T : struct, IComponent
        {
            entity.MyWorld.World.AddComponent(entity, ref component, writeType == WriteType.ReWrite);

            return entity;
        }

        public static Entity AddComponent<T>(this Entity entity, T component, WriteType writeType = WriteType.Write)
            where T : struct, IComponent
        {
            entity.MyWorld.World.AddComponent(entity, ref component, writeType == WriteType.ReWrite);

            return entity;
        }

        public static Entity AddComponent<T>(this Entity entity, SetupComponentAction<T> callback)
            where T : struct, IComponent
        {
            var component = Activator.CreateInstance<T>();
            callback?.Invoke(ref component);
            entity.AddComponent(ref component);

            return entity;
        }

        public static Entity RemoveComponent<T>(this Entity entity)
            where T : struct, IComponent
        {
            entity.MyWorld.World.RemoveComponent<T>(entity);

            return entity;
        }
    }
}
