using LETO.ECS.Worlds;

namespace LETO.ECS
{
    public ref struct ECSProxy
    {
        internal ECSProxy(World world)
        {
            World = world;
        }

        internal World World { get; }
        public bool IsLived => !World.IsDisposed;

        public Filter<T1> CreateFilter<T1>()
           where T1 : struct, IComponent
        {
            return World.CreateFilter<T1>();
        }

        public Filter<T1, T2> CreateFilter<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            return World.CreateFilter<T1, T2>();
        }

        public Filter<T1, T2, T3> CreateFilter<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            return World.CreateFilter<T1, T2, T3>();
        }
        public Filter<T1, T2, T3, T4> CreateFilter<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            return World.CreateFilter<T1, T2, T3, T4>();
        }
        public Filter<T1, T2, T3, T4, T5> CreateFilter<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
        {
            return World.CreateFilter<T1, T2, T3, T4, T5>();
        }

        public Filter<T1, T2, T3, T4, T5, T6> CreateFilter<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
            where T5 : struct, IComponent
            where T6 : struct, IComponent
        {
            return World.CreateFilter<T1, T2, T3, T4, T5, T6>();
        }
    }
}
