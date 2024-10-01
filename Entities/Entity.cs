using LETO.ECS.Worlds;

namespace LETO.ECS
{
    public sealed class Entity
    {
        internal Entity(World world) => World = world;
        internal World World { get; }
    }
}
