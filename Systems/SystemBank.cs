using System;
using System.Collections.Generic;
using System.Linq;

namespace LETO.ECS.Systems
{
    internal sealed class SystemBank : IDisposable
    {
        private readonly Dictionary<SystemUpdateType, List<ISystem>> Systems;

        internal SystemBank()
        {
            Systems = new Dictionary<SystemUpdateType, List<ISystem>>()
            {
                {SystemUpdateType.Update, new List<ISystem>() },
                {SystemUpdateType.FixedUpdate, new List<ISystem>() },
                {SystemUpdateType.LateUpdate, new List<ISystem>() }
            };
        }

        public void Dispose()
        {
            foreach (var set in Systems.Values) 
            {
                set.Clear();
            }

            Systems.Clear();
            GC.SuppressFinalize(this);
        }

        internal void EnableSystem<T>(SystemUpdateType updateType, T system)
            where T : class, ISystem
        {
            Systems[updateType].Add(system);
            Systems[updateType].Sort();
        }

        internal void DisableSystem<T>()
            where T : class, ISystem
        {
            foreach(var list in Systems.Values)
            {
                list.RemoveAll(x => x.GetType().Equals(typeof(T)));
                list.Sort();
            }
        }

        internal IReadOnlyList<ISystem> GetSystems(SystemUpdateType updateType) => Systems[updateType];
    }
}
