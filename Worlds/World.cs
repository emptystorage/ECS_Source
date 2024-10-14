using System;
using System.Collections.Generic;
using LETO.ECS.Entities;
using LETO.ECS.Components;
using LETO.ECS.Systems;

namespace LETO.ECS.Worlds
{
    internal sealed class World : IDisposable
    {
        private readonly IWorldTick WorldTick;
        private readonly ISystemFactory SystemFactory;

        private readonly ComponentBank ComponentBank;
        private readonly SystemBank SystemBank;
        private readonly EntityBank EntityBank;

        internal World(IWorldTick worldTick, ISystemFactory systemFactory, string tag, int componentSize)
        {

            ComponentBank = new ComponentBank(componentSize);
            SystemBank = new SystemBank();
            EntityBank = new EntityBank();

            WorldTick = worldTick;
            SystemFactory = systemFactory;
            Tag = tag;

            WorldTick.UpdateTick += OnUpdate;
            WorldTick.FixedUpdateTick += OnFixedUpdate;
            WorldTick.LateUpdateTick += OnLateUpdate;
        }

        ~World()
        {
            WorldTick.UpdateTick -= OnUpdate;
            WorldTick.FixedUpdateTick -= OnFixedUpdate;
            WorldTick.LateUpdateTick -= OnLateUpdate;
        }

        internal string Tag { get; }
        internal bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;

            EntityBank.Dispose();
            ComponentBank.Dispose();
            SystemBank.Dispose();


            WorldTick.UpdateTick -= OnUpdate;
            WorldTick.FixedUpdateTick -= OnFixedUpdate;
            WorldTick.LateUpdateTick -= OnLateUpdate;

            GC.SuppressFinalize(this);
        }

        internal Entity CreateEntity()
        {
            return EntityBank.Create(this);
        }

        internal void DestroyEntity(in Entity entity)
        {
            ComponentBank.ExcuteClearn(entity);
            EntityBank.Destroy(entity);
        }

        internal void AddComponent<T>(Entity entity, ref T component, bool isIgnoreBinded)
            where T : struct, IComponent
        {
            if(ComponentBank.TryGetComponentTable<T>(out var table))
            {
                if (isIgnoreBinded && table.ContainOf(entity))
                {
                    table.Forget(entity);
                }

                table.Bind(entity, ref component);
                ComponentBank.AddEntityToClearn(entity, table);
            }
        }

        internal void RemoveComponent<T>(Entity entity)
            where T : struct, IComponent
        {

            if(ComponentBank.TryGetComponentTable<T>(out var table) 
                && table.ContainOf(entity))
            {
                ComponentBank.RemoveEntityToCLearn(entity, table);
                table.Forget(entity);
            }
        }

        internal void EnableSystem<T>(SystemUpdateType updateType)
            where T: ECSSystem
        {
            var system = SystemFactory.Create<T>();
            system.World = this;
            system.Setup();

            SystemBank.EnableSystem(updateType, system);
        }

        internal void DisableSystem<T>()
            where T: ECSSystem
        {
            SystemBank.DisableSystem<T>();
        }

        internal void EnableComponent<T>()
            where T : struct, IComponent
        {
            ComponentBank.EnableComponent<T>();
        }

        internal void DisableComponent<T>()
            where T : struct, IComponent
        {
            ComponentBank.DisableComponent<T>();
        }

        #region Filters
        internal Filter<T1> CreateFilter<T1>()
            where T1 : struct, IComponent
        {
            if(ComponentBank.TryGetComponentTable<T1>(out var table))
            {
                return new Filter<T1>(table);
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }

        internal Filter<T1, T2> CreateFilter<T1, T2>()
            where T1: struct, IComponent
            where T2: struct, IComponent
        {
            if (ComponentBank.TryGetComponentTable<T1>(out var tableI))
            {
                if(ComponentBank.TryGetComponentTable<T2>(out var tableII))
                {
                    return new Filter<T1, T2>(tableI, tableII);
                }
                else
                {
                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T2).Name}");
                }
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }

        internal Filter<T1, T2, T3> CreateFilter<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            if (ComponentBank.TryGetComponentTable<T1>(out var tableI))
            {
                if (ComponentBank.TryGetComponentTable<T2>(out var tableII))
                {
                    if(ComponentBank.TryGetComponentTable<T3>(out var tableIII))
                    {
                        return new Filter<T1, T2, T3>(tableI, tableII, tableIII);
                    }
                    else
                    {
                        throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T3).Name}");
                    }
                }
                else
                {
                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T2).Name}");
                }
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }

        internal Filter<T1, T2, T3, T4> CreateFilter<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            if (ComponentBank.TryGetComponentTable<T1>(out var tableI))
            {
                if (ComponentBank.TryGetComponentTable<T2>(out var tableII))
                {
                    if (ComponentBank.TryGetComponentTable<T3>(out var tableIII))
                    {
                        if (ComponentBank.TryGetComponentTable<T4>(out var tableIV))
                        {
                            return new Filter<T1, T2, T3, T4>(tableI, tableII, tableIII, tableIV);
                        }
                        else
                        {
                            throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T4).Name}");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T3).Name}");
                    }
                }
                else
                {
                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T2).Name}");
                }
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }

        internal Filter<T1, T2, T3, T4, T5> CreateFilter<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            if (ComponentBank.TryGetComponentTable<T1>(out var tableI))
            {
                if (ComponentBank.TryGetComponentTable<T2>(out var tableII))
                {
                    if (ComponentBank.TryGetComponentTable<T3>(out var tableIII))
                    {
                        if (ComponentBank.TryGetComponentTable<T4>(out var tableIV))
                        {
                            if (ComponentBank.TryGetComponentTable<T5>(out var tableV))
                            {
                                return new Filter<T1, T2, T3, T4, T5>(tableI, tableII, tableIII, tableIV, tableV);
                            }
                            else
                            {
                                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T5).Name}");
                            }
                        }
                        else
                        {
                            throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T4).Name}");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T3).Name}");
                    }
                }
                else
                {
                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T2).Name}");
                }
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }

        internal Filter<T1, T2, T3, T4, T5,T6> CreateFilter<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            if (ComponentBank.TryGetComponentTable<T1>(out var tableI))
            {
                if (ComponentBank.TryGetComponentTable<T2>(out var tableII))
                {
                    if (ComponentBank.TryGetComponentTable<T3>(out var tableIII))
                    {
                        if (ComponentBank.TryGetComponentTable<T4>(out var tableIV))
                        {
                            if (ComponentBank.TryGetComponentTable<T5>(out var tableV))
                            {
                                if(ComponentBank.TryGetComponentTable<T6>(out var tableVI))
                                {
                                    return new Filter<T1, T2, T3, T4, T5, T6>(tableI, tableII, tableIII, tableIV, tableV, tableVI);
                                }
                                else
                                {
                                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T6).Name}");
                                }
                            }
                            else
                            {
                                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T5).Name}");
                            }
                        }
                        else
                        {
                            throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T4).Name}");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T3).Name}");
                    }
                }
                else
                {
                    throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T2).Name}");
                }
            }
            else
            {
                throw new NullReferenceException($"Мир с тегом - {Tag} не содержит компонент - {typeof(T1).Name}");
            }
        }
        #endregion

        #region Update
        private void OnUpdate() => OnUpdateSystems(SystemBank.GetSystems(SystemUpdateType.Update));

        private void OnFixedUpdate() => OnUpdateSystems(SystemBank.GetSystems(SystemUpdateType.FixedUpdate));

        private void OnLateUpdate() => OnUpdateSystems(SystemBank.GetSystems(SystemUpdateType.LateUpdate));

        private void OnUpdateSystems(IReadOnlyList<ISystem> systems)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].Update();
            }
        }
        #endregion
    }
}
