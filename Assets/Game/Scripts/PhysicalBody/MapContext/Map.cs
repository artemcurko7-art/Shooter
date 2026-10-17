using System;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.MapContext
{
    public class Map : PhysicalBody<Map>
    {
        [field: SerializeField] public Transform Road { get; private set; }
        
        public Action<Map> Released;
    }
}