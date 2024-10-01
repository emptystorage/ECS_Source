using System;
using LETO.ECS.Entities;

namespace LETO.ECS
{
    public enum ComponentAddType
    {
        Binded,
        IgnoreBinded
    }

    public static class EntityExtension
    {
        public static void Destroy(this Entity entity)
        {
            entity.World.DestroyEntity(entity);
        }

        public static Entity AddComponent<T>(this Entity entity, ComponentAddType addType = ComponentAddType.Binded)
            where T : struct, IComponent
        {
            var component = Activator.CreateInstance<T>();
            entity.AddComponent(ref component, addType);

            return entity;
        }

        public static Entity AddComponent<T>(this Entity entity, ref T component, ComponentAddType addType = ComponentAddType.Binded)
            where T : struct, IComponent
        {
            entity.World.AddComponent(entity, ref component, addType == ComponentAddType.IgnoreBinded);

            return entity;
        }

        public static Entity AddComponent<T>(this Entity entity, T component, ComponentAddType addType = ComponentAddType.Binded)
            where T : struct, IComponent
        {
            entity.World.AddComponent(entity, ref component, addType == ComponentAddType.IgnoreBinded);

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
            entity.World.RemoveComponent<T>(entity);

            return entity;
        }
    }
}
