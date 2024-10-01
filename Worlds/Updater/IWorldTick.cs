using System;

namespace LETO.ECS
{
    public interface IWorldTick
    {
        event Action UpdateTick;
        event Action FixedUpdateTick;
        event Action LateUpdateTick;
    }
}
