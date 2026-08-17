using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class SlotFactory
    {
        private readonly Slot _slot;
        private readonly DiContainer _container;
        
        public SlotFactory(Slot slot, DiContainer container)
        {
            _slot = slot;
            _container = container;
        }

        public Slot Create(RarityEquipmentConfig config, Sprite icon, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<Slot>(_slot, Vector3.zero, Quaternion.identity, container);
            view.Initialize(config.Icon, icon);
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}