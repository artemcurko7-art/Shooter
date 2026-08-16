using Game.Scripts.Equipment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class RarityEquipmentViewFactory
    {
        private readonly DiContainer _container;
        
        public RarityEquipmentViewFactory(DiContainer container)
        {
            _container = container;
        }

        public RarityEquipmentView Create(RarityEquipmentView rarityEquipmentView, Sprite icon, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<RarityEquipmentView>(rarityEquipmentView, Vector3.zero, Quaternion.identity, container);
            view.Initialize(icon);
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}