using Game.Scripts.PhysicalBody;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Service.PhysicalBody
{
    public abstract class PhysicalBodyService<T> : ISubscriber where T : PhysicalBody<T>
    {
        protected readonly float Delay;
    
        public PhysicalBodyService(float delay)
        {
            Delay = delay;
        }
    
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}