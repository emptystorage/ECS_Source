using System;
using System.Collections.Generic;
using LETO.ECS.Worlds;

namespace LETO.ECS.Entities
{
    internal sealed class EntityBank : IDisposable
    {
        private readonly Stack<Entity> FreeEntities = new Stack<Entity>();
        private readonly LinkedList<Entity> UsedEntities = new LinkedList<Entity>();

        public void Dispose()
        {
            UsedEntities.Clear();
            FreeEntities.Clear();

            GC.SuppressFinalize(this);
        }

        internal Entity Create(World world)
        {
            if(FreeEntities.Count > 0)
            {
                UsedEntities.AddLast(FreeEntities.Peek());
                return FreeEntities.Pop();
            }
            else
            {
                UsedEntities.AddLast(new Entity(world));
                return UsedEntities.Last.Value;
            }
        }

        internal void Destroy(in Entity entity)
        {
            UsedEntities.Remove(entity);
            FreeEntities.Push(entity);
        }
    }
}
