using Game.Scripts.Equipment.CharacterContext;
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
        
        public Character Create(Character character, Transform container)
        {
            var model = _container.InstantiatePrefabForComponent<Character>(character, container.position, Quaternion.identity, container);
            
            return model;
        }
    }
}