using System;

namespace Game.Scripts.PlayerContext.Death
{
    public interface IDeath
    {
        public event Action Dead;
    }
}