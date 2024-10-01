using System;
using System.Collections.Generic;
using System.Linq;
using LETO.ECS.Entities;

namespace LETO.ECS.Components
{
    internal sealed class ComponentTable<T> : IDisposable
        where T : struct, IComponent
    {
        private readonly Dictionary<int, T[]> Tables;
        private readonly Dictionary<Entity, int> BindedEntities;
        private readonly Stack<int> FreeComponentIndex;
        private readonly int ComponentsArraySize;
        private int _maxComponentIndex;

        internal ComponentTable(int componentsArraySize)
        {
            ComponentsArraySize = componentsArraySize;
            Tables = new Dictionary<int, T[]>();
            BindedEntities = new Dictionary<Entity, int>();
            FreeComponentIndex = new Stack<int>();
        }

        internal IReadOnlyList<Entity> Entities => BindedEntities.Keys.ToArray();
        internal int EntitiesCount => BindedEntities.Keys.Count;

        public void Dispose()
        {
            foreach (var table in Tables.Values)
            {
                Array.Clear(table, default, table.Length);
            }

            Tables.Clear();
            BindedEntities.Clear();
            FreeComponentIndex.Clear();

            GC.SuppressFinalize(this);
        }

        internal bool ContainOf(Entity entity)
        {
            return BindedEntities.ContainsKey(entity);
        }

        internal void Bind(in Entity entity, ref T component)
        {
            if (!BindedEntities.ContainsKey(entity))
            {
                var index = FreeComponentIndex.Count > 0 ? FreeComponentIndex.Pop() : _maxComponentIndex++;
                BindedEntities.Add(entity, index);                

                var indexData = GetIndexData(index);

                if (!Tables.TryGetValue(indexData.tableIndex, out var components))
                {
                    components = new T[ComponentsArraySize];
                    Tables[indexData.tableIndex] = components;
                }

                components[indexData.componentIndex] = component;
            }
        }

        internal ref T Get(in Entity entity)
        {
            var indexData = GetIndexData(BindedEntities[entity]);
            return ref Tables[indexData.tableIndex][indexData.componentIndex];
        }


        internal void Forget(Entity entity)
        {
            if (BindedEntities.TryGetValue(entity, out var index))
            {
                FreeComponentIndex.Push(index);
                BindedEntities.Remove(entity);
            }            
        }

        private (int tableIndex, int componentIndex) GetIndexData(int componentIndex, int tableIndex = 0)
        {
            if(componentIndex >= ComponentsArraySize)
            {
                return GetIndexData(componentIndex - ComponentsArraySize, ++tableIndex);
            }
            else
            {
                return (tableIndex, componentIndex);
            }
        }        
    }
}
