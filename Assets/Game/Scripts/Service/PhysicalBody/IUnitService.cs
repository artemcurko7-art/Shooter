using System.Collections.Generic;
using Game.Scripts.PlayerContext;

namespace Game.Scripts.Service.PhysicalBody
{
    public interface IUnitService 
    {
        public IReadOnlyList<ITransformable> Units { get; }
    }
}