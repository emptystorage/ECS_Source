using System;
using System.Collections.Generic;
using LETO.ECS.Entities;
using LETO.ECS.Worlds;

namespace LETO.ECS
{
    public delegate void SetupComponentAction<T>(ref T component) where T : struct, IComponent;

    public static class ECSControl
    {
        private static Dictionary<string, World> Worlds = new Dictionary<string, World>();

        public static ECSProxy CreateWorld(IWorldTick updater, ISystemFactory systemFactory = null, string worldTag = "default", int componentSize = 16)
        {
            if(systemFactory == null)
            {
                systemFactory = new DefaultSystemFactory();
            }

            if(!Worlds.TryGetValue(worldTag, out var world))
            {
                world = new World(updater, systemFactory, worldTag, componentSize);
                Worlds.Add(worldTag, world);
            }

            return new ECSProxy(world);
        }

        public static ECSProxy GetWorld(string worldTag = "default")
        {
            if(Worlds.TryGetValue(worldTag, out var world))
            {
                return new ECSProxy(world);
            }
            else
            {
                throw new NullReferenceException($"Не создан мир с тегом - {worldTag}");
            }
        }

        public static void DestroyWorld(this ECSProxy proxy)
        {
            Worlds.Remove(proxy.World.Tag);
            proxy.World.Dispose();
        }

        public static void DestroyWorld(string worldTag = "default")
        {
            if(Worlds.TryGetValue(worldTag, out var world))
            {
                world.Dispose();
                Worlds.Remove(worldTag);
            }
        }

        public static ECSProxy EnableSystem<T>(this ECSProxy proxy, SystemUpdateType updateType = SystemUpdateType.Update)
            where T : ECSSystem
        {
            proxy.World.EnableSystem<T>(updateType);
            return proxy;
        }

        public static ECSProxy DisableSystem<T>(this ECSProxy proxy)
            where T : ECSSystem
        {
            proxy.World.DisableSystem<T>();

            return proxy;
        }

        public static Entity CreateEntity(this ECSProxy proxy)
        {
            return proxy.World.CreateEntity();
        }
    }
}
