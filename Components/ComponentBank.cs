using System;
using System.Collections.Generic;
using LETO.ECS.Entities;

namespace LETO.ECS.Components
{
    internal sealed class ComponentBank : IDisposable
    {
        private readonly Dictionary<Type, IDisposable> Tables = new Dictionary<Type, IDisposable>();
        private readonly Dictionary<Entity, Action<Entity>> ClearnEntityTable = new Dictionary<Entity, Action<Entity>>();
        private readonly int Size;

        internal ComponentBank(int size)
        {
            Size = size;
        }

        public void Dispose()
        {
            foreach (var table in Tables.Values)
            {
                table.Dispose();
            }

            ClearnEntityTable.Clear();
            Tables.Clear();

            GC.SuppressFinalize(this);
        }

        internal ComponentTable<T> EnableComponent<T>()
            where T : struct, IComponent
        {
            var table = new ComponentTable<T>(Size);
            Tables.TryAdd(typeof(T), table);

            return table;
        }

        internal void DisableComponent<T>()
            where T : struct, IComponent
        {
            if(Tables.TryGetValue(typeof(T), out var table))
            {
                table.Dispose();
                Tables.Remove(typeof(T));
            }
        }

        internal bool TryGetComponentTable<T>(out ComponentTable<T> table)
            where T : struct, IComponent
        {
            table = null;

            if(Tables.TryGetValue(typeof(T), out var result))
            {
                table = result as ComponentTable<T>;
            }

            return table != null;
        }

        internal void AddEntityToClearn<T>(in Entity entity, ComponentTable<T> table)
            where T : struct, IComponent
        {
            if (!ClearnEntityTable.ContainsKey(entity))
            {
                ClearnEntityTable[entity] = entity => { };
            }

            ClearnEntityTable[entity] += table.Forget;
        }

        internal void RemoveEntityToCLearn<T>(in Entity entity, ComponentTable<T> table)
            where T : struct, IComponent
        {
            if(ClearnEntityTable.ContainsKey(entity))
            {
                ClearnEntityTable[entity] -= table.Forget;
            }
        }

        internal void ExcuteClearn(in Entity entity)
        {
            if (ClearnEntityTable.ContainsKey(entity))
            {
                ClearnEntityTable[entity].Invoke(entity);
            }

            ClearnEntityTable.Remove(entity);
        }
    }
}
