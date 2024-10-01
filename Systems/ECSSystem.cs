using System;
using LETO.ECS.Entities;
using LETO.ECS.Worlds;
using LETO.ECS.Systems;

namespace LETO.ECS
{
    public abstract class ECSSystem : ISystem
    {
        public virtual int Order { get; } = default;
        public ECSProxy MyWorld => new ECSProxy(World);

        internal World World { get; set; }

        int IComparable<ISystem>.CompareTo(ISystem other)
        {
            if (this.Order == other.Order) return 0;
            if (this.Order > other.Order) return 1;
            else return -1;
        }

        public abstract void Update();
        protected internal virtual void Setup() { }
    }

    public abstract class ECSSystem<T1> : ECSSystem
        where T1 : struct, IComponent
    {
        public override void Update()
        {
            if (World.IsDisposed) return;

            var filter = World.CreateFilter<T1>();

            while(filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(), 
                    ref filter.GetComponentI(filter.Entities.Dequeue()));
            }
        }

        protected abstract void Update(in Entity entity, ref T1 component);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
        }
    }

    public abstract class ECSSystem<T1,T2> : ECSSystem
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        public override void Update()
        {
            if(World.IsDisposed) return;

            var filter = World.CreateFilter<T1, T2>();

            while(filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(),
                    ref filter.GetComponentI(filter.Entities.Peek()),
                    ref filter.GetComponentII(filter.Entities.Dequeue())
                    );
            }
        }

        protected abstract void Update(in Entity entity, ref T1 componentI, ref T2 componentII);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
            World.EnableComponent<T2>();
        }
    }

    public abstract class ECSSystem<T1, T2, T3> : ECSSystem
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        public override void Update()
        {
            if (World.IsDisposed) return;

            var filter = World.CreateFilter<T1, T2, T3>();

            while (filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(),
                    ref filter.GetComponentI(filter.Entities.Peek()),
                    ref filter.GetComponentII(filter.Entities.Peek()),
                    ref filter.GetComponentIII(filter.Entities.Dequeue())
                    );
            }
        }

        protected abstract void Update(in Entity entity, ref T1 componentI, ref T2 componentII, ref T3 componentIII);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
            World.EnableComponent<T2>();
            World.EnableComponent<T3>();
        }
    }
    public abstract class ECSSystem<T1, T2, T3, T4> : ECSSystem
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        public override void Update()
        {
            if (World.IsDisposed) return;

            var filter = World.CreateFilter<T1, T2, T3, T4>();

            while (filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(),
                    ref filter.GetComponentI(filter.Entities.Peek()),
                    ref filter.GetComponentII(filter.Entities.Peek()),
                    ref filter.GetComponentIII(filter.Entities.Peek()),
                    ref filter.GetComponentIV(filter.Entities.Dequeue())
                    );
            }
        }

        protected abstract void Update(in Entity entity, ref T1 componentI, ref T2 componentII, ref T3 componentIII, ref T4 componentIV);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
            World.EnableComponent<T2>();
            World.EnableComponent<T3>();
            World.EnableComponent<T4>();
        }
    }

    public abstract class ECSSystem<T1, T2, T3, T4, T5> : ECSSystem
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
    {
        public override void Update()
        {
            if (World.IsDisposed) return;

            var filter = World.CreateFilter<T1, T2, T3, T4, T5>();

            while (filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(),
                    ref filter.GetComponentI(filter.Entities.Peek()),
                    ref filter.GetComponentII(filter.Entities.Peek()),
                    ref filter.GetComponentIII(filter.Entities.Peek()),
                    ref filter.GetComponentIV(filter.Entities.Peek()),
                    ref filter.GetComponentV(filter.Entities.Dequeue())
                    );
            }
        }

        protected abstract void Update(in Entity entity, ref T1 componentI, ref T2 componentII, ref T3 componentIII, ref T4 componentIV, ref T5 componentV);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
            World.EnableComponent<T2>();
            World.EnableComponent<T3>();
            World.EnableComponent<T4>();
            World.EnableComponent<T5>();
        }
    }

    public abstract class ECSSystem<T1, T2, T3, T4, T5, T6> : ECSSystem
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
        where T5 : struct, IComponent
        where T6 : struct, IComponent
    {
        public override void Update()
        {
            if (World.IsDisposed) return;

            var filter = World.CreateFilter<T1, T2, T3, T4, T5, T6>();

            while (filter.Entities.Count > 0)
            {
                Update(
                    filter.Entities.Peek(),
                    ref filter.GetComponentI(filter.Entities.Peek()),
                    ref filter.GetComponentII(filter.Entities.Peek()),
                    ref filter.GetComponentIII(filter.Entities.Peek()),
                    ref filter.GetComponentIV(filter.Entities.Peek()),
                    ref filter.GetComponentV(filter.Entities.Peek()),
                    ref filter.GetComponentVI(filter.Entities.Dequeue())
                    );
            }
        }

        protected abstract void Update(in Entity entity, ref T1 componentI, ref T2 componentII, ref T3 componentIII, ref T4 componentIV, ref T5 componentV, ref T6 componentVI);
        protected internal override void Setup()
        {
            World.EnableComponent<T1>();
            World.EnableComponent<T2>();
            World.EnableComponent<T3>();
            World.EnableComponent<T4>();
            World.EnableComponent<T5>();
            World.EnableComponent<T6>();
        }
    }
}
