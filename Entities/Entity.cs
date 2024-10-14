using LETO.ECS.Worlds;

namespace LETO.ECS
{
    public sealed class Entity
    {
        private readonly World _world;

        internal Entity(World world) => _world = world;

        public ECSProxy MyWorld => new ECSProxy(_world);
    }
}
