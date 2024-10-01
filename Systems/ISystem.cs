using System;

namespace LETO.ECS.Systems
{
    internal interface ISystem : IComparable<ISystem>
    {
        int Order { get; }
        void Update();
    }
}
