using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Service.PhysicalBody
{
    public class ExampleKeyboardMapService : MonoBehaviour
    {
        private MapService _mapService;

        [Inject]
        public void Construct(MapService mapService)
        {
            _mapService = mapService;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
                _mapService.SpawnForward();

            if (Input.GetKeyDown(KeyCode.C))
                _mapService.SpawnBack();
        }
    }
}