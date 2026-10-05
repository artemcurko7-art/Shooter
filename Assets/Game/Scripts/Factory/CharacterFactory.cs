using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.WeaponContext.Type;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Factory
{
    public class CharacterFactory
    {
        private readonly DiContainer _container;

        public CharacterFactory(DiContainer container)
        {
            _container = container;
        }
        
        public Character Create(Character character, WeaponType weaponType, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<Character>(character, container.position, Quaternion.identity, container);
            view.Initialize(weaponType);
            
            return view;
        }
    }
}