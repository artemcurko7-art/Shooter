using System;
using Game.Scripts.PhysicalBodyContext;
using UnityEngine;

namespace Game.Scripts.MapContext
{
    public class Map : PhysicalBody<Map>
    {
        [field: SerializeField] public Transform Road { get; private set; }
        
        public Action<Map> Released;
    }
}