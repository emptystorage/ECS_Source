using System;
using System.Collections.Generic;
using LETO.ECS.Components;
using LETO.ECS.Entities;

namespace LETO.ECS
{
    internal static class FilterHandler
    {
        internal static int GetMinimalIndexArray(int[] array)
        {
            int index = 0;
            int value = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (value > array[i])
                {
                    index = i;
                    value = array[i];
                }
            }

            return index;
        }

        internal static void FillEntities(in Queue<Entity> entityQueue, IReadOnlyList<Entity> entitiesCheck, Predicate<Entity> checkCallback)
        {
            for (int i = 0; i < entitiesCheck.Count; i++)
            {
                if (checkCallback.Invoke(entitiesCheck[i]))
                {
                    entityQueue.Enqueue(entitiesCheck[i]);
                }
            }
        }
    }

    public readonly ref struct Filter<T1>
        where T1 : struct, IComponent
    {
        private readonly ComponentTable<T1> Table;

        internal Filter(ComponentTable<T1> table)
        {
            Table = table;
            Entities = new Queue<Entity>(table.Entities);
        }        
        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref Table.Get(entity);
    }

    public readonly ref struct Filter<T1, T2>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        private readonly ComponentTable<T1> TableI;
        private readonly ComponentTable<T2> TableII;

        internal Filter(ComponentTable<T1> tableI, ComponentTable<T2> tableII)
        {
            TableI = tableI;
            TableII = tableII;

            Entities = new Queue<Entity>();
            
            if(tableI.EntitiesCount > tableII.EntitiesCount)
            {
                FilterHandler.FillEntities(Entities, tableII.Entities, tableI.ContainOf);
            }
            else
            {
                FilterHandler.FillEntities(Entities, tableI.Entities, tableII.ContainOf);
            }
        }

        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref TableI.Get(entity);
        public ref T2 GetComponentII(in Entity entity) => ref TableII.Get(entity);
    }

    public readonly ref struct Filter<T1, T2, T3>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        private readonly ComponentTable<T1> TableI;
        private readonly ComponentTable<T2> TableII;
        private readonly ComponentTable<T3> TableIII;

        internal Filter(ComponentTable<T1> tableI, ComponentTable<T2> tableII, ComponentTable<T3> tableIII)
        {
            TableI = tableI;
            TableII = tableII;
            TableIII = tableIII;

            Entities = new Queue<Entity>();
            var minTableIndex = FilterHandler.GetMinimalIndexArray(new int[] { tableI.EntitiesCount, tableII.EntitiesCount, tableIII.EntitiesCount });

            switch (minTableIndex)
            {
                case 0:
                    FilterHandler.FillEntities(Entities, tableI.Entities, entity => { return tableII.ContainOf(entity) && tableIII.ContainOf(entity); });
                    break;
                case 1:
                    FilterHandler.FillEntities(Entities, tableII.Entities, entity => { return tableI.ContainOf(entity) && tableIII.ContainOf(entity); });
                    break;
                case 2:
                    FilterHandler.FillEntities(Entities, tableIII.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity); });
                    break;
            }
        }

        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref TableI.Get(entity);
        public ref T2 GetComponentII(in Entity entity) => ref TableII.Get(entity);
        public ref T3 GetComponentIII(in Entity entity) => ref TableIII.Get(entity);        
    }

    public readonly ref struct Filter<T1, T2, T3, T4>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        private readonly ComponentTable<T1> TableI;
        private readonly ComponentTable<T2> TableII;
        private readonly ComponentTable<T3> TableIII;
        private readonly ComponentTable<T4> TableIV;

        internal Filter(ComponentTable<T1> tableI, ComponentTable<T2> tableII, ComponentTable<T3> tableIII, ComponentTable<T4> tableIV)
        {
            TableI = tableI;
            TableII = tableII;
            TableIII = tableIII;
            TableIV = tableIV;

            Entities = new Queue<Entity>();
            var minTableIndex = FilterHandler.GetMinimalIndexArray(new int[] { tableI.EntitiesCount, tableII.EntitiesCount, tableIII.EntitiesCount, tableIV.EntitiesCount });

            switch (minTableIndex)
            {
                case 0:
                    FilterHandler.FillEntities(Entities, tableI.Entities, entity => { return tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity); });
                    break;
                case 1:
                    FilterHandler.FillEntities(Entities, tableII.Entities, entity => { return tableI.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity); });
                    break;
                case 2:
                    FilterHandler.FillEntities(Entities, tableIII.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIV.ContainOf(entity); });
                    break;
                case 3:
                    FilterHandler.FillEntities(Entities, tableIV.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity); });
                    break;
            }
        }

        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref TableI.Get(entity);
        public ref T2 GetComponentII(in Entity entity) => ref TableII.Get(entity);
        public ref T3 GetComponentIII(in Entity entity) => ref TableIII.Get(entity);
        public ref T4 GetComponentIV(in Entity entity) => ref TableIV.Get(entity);
    }

    public readonly ref struct Filter<T1, T2, T3, T4, T5>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
    {
        private readonly ComponentTable<T1> TableI;
        private readonly ComponentTable<T2> TableII;
        private readonly ComponentTable<T3> TableIII;
        private readonly ComponentTable<T4> TableIV;
        private readonly ComponentTable<T5> TableV;

        internal Filter(ComponentTable<T1> tableI, ComponentTable<T2> tableII, ComponentTable<T3> tableIII, ComponentTable<T4> tableIV, ComponentTable<T5> tableV)
        {
            TableI = tableI;
            TableII = tableII;
            TableIII = tableIII;
            TableIV = tableIV;
            TableV = tableV;

            Entities = new Queue<Entity>();
            var minTableIndex = FilterHandler.GetMinimalIndexArray(new int[] { tableI.EntitiesCount, tableII.EntitiesCount, tableIII.EntitiesCount, tableIV.EntitiesCount, tableV.EntitiesCount });

            switch (minTableIndex)
            {
                case 0:
                    FilterHandler.FillEntities(Entities, tableI.Entities, entity => { return tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity); });
                    break;
                case 1:
                    FilterHandler.FillEntities(Entities, tableII.Entities, entity => { return tableI.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity); });
                    break;
                case 2:
                    FilterHandler.FillEntities(Entities, tableIII.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity); });
                    break;
                case 3:
                    FilterHandler.FillEntities(Entities, tableIV.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableV.ContainOf(entity); });
                    break;
                case 4:
                    FilterHandler.FillEntities(Entities, tableV.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity); });
                    break;
            }
        }

        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref TableI.Get(entity);
        public ref T2 GetComponentII(in Entity entity) => ref TableII.Get(entity);
        public ref T3 GetComponentIII(in Entity entity) => ref TableIII.Get(entity);
        public ref T4 GetComponentIV(in Entity entity) => ref TableIV.Get(entity);
        public ref T5 GetComponentV(in Entity entity) => ref TableV.Get(entity);
    }

    public readonly ref struct Filter<T1, T2, T3, T4, T5,T6>
       where T1 : struct, IComponent
       where T2 : struct, IComponent
       where T3 : struct, IComponent
       where T4 : struct, IComponent
       where T5 : struct, IComponent
       where T6 : struct, IComponent   
    {
        private readonly ComponentTable<T1> TableI;
        private readonly ComponentTable<T2> TableII;
        private readonly ComponentTable<T3> TableIII;
        private readonly ComponentTable<T4> TableIV;
        private readonly ComponentTable<T5> TableV;
        private readonly ComponentTable<T6> TableVI;

        internal Filter(ComponentTable<T1> tableI, ComponentTable<T2> tableII, ComponentTable<T3> tableIII, ComponentTable<T4> tableIV, ComponentTable<T5> tableV, ComponentTable<T6> tableVI)
        {
            TableI = tableI;
            TableII = tableII;
            TableIII = tableIII;
            TableIV = tableIV;
            TableV = tableV;
            TableVI = tableVI;

            Entities = new Queue<Entity>();
            var minTableIndex = FilterHandler.GetMinimalIndexArray(new int[] { tableI.EntitiesCount, tableII.EntitiesCount, tableIII.EntitiesCount, tableIV.EntitiesCount, tableV.EntitiesCount, tableVI.EntitiesCount });

            switch (minTableIndex)
            {
                case 0:
                    FilterHandler.FillEntities(Entities, tableI.Entities, entity => { return tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity) && tableVI.ContainOf(entity); });
                    break;
                case 1:
                    FilterHandler.FillEntities(Entities, tableII.Entities, entity => { return tableI.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity) && tableVI.ContainOf(entity); });
                    break;
                case 2:
                    FilterHandler.FillEntities(Entities, tableIII.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity) && tableVI.ContainOf(entity); });
                    break;
                case 3:
                    FilterHandler.FillEntities(Entities, tableIV.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableV.ContainOf(entity) && tableVI.ContainOf(entity); });
                    break;
                case 4:
                    FilterHandler.FillEntities(Entities, tableV.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableVI.ContainOf(entity); });
                    break;
                case 5:
                    FilterHandler.FillEntities(Entities, tableVI.Entities, entity => { return tableI.ContainOf(entity) && tableII.ContainOf(entity) && tableIII.ContainOf(entity) && tableIV.ContainOf(entity) && tableV.ContainOf(entity); });
                    break;
            }
        }

        public Queue<Entity> Entities { get; }

        public ref T1 GetComponentI(in Entity entity) => ref TableI.Get(entity);
        public ref T2 GetComponentII(in Entity entity) => ref TableII.Get(entity);
        public ref T3 GetComponentIII(in Entity entity) => ref TableIII.Get(entity);
        public ref T4 GetComponentIV(in Entity entity) => ref TableIV.Get(entity);
        public ref T5 GetComponentV(in Entity entity) => ref TableV.Get(entity);
        public ref T6 GetComponentVI(in Entity entity) => ref TableVI.Get(entity);
    }
}
