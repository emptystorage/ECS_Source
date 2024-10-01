using System;
using System.Threading.Tasks;

namespace LETO.ECS
{
    public sealed class DefaultWorldTick : IWorldTick
    {
        public event Action UpdateTick;
        public event Action FixedUpdateTick;
        public event Action LateUpdateTick;

        public async Task Update()
        {
            while (true)
            {
                await Task.Delay(1000);
                UpdateTick?.Invoke();
                FixedUpdateTick?.Invoke();
                LateUpdateTick?.Invoke();
            }
        }
    }
}
