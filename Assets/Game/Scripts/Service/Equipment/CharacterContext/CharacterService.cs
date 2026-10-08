using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.SquadContext;
using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Data;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public class CharacterService : ICharacterService
    {
        private const float FormationSpacing = 1.5f;

        private readonly ICharacterData _data;
        private readonly ICharacterProvider _provider;
        private readonly CharacterFactory _factory;
        private readonly WeaponData _weaponData;
        private readonly WeaponViewFactory _weaponViewFactory;
        private readonly SquadPosition[] _squadPositions;
        private readonly List<Character> _characters = new();
        private readonly List<WeaponView> _weaponViews = new();
        private readonly Dictionary<int, Vector3[]> Offsets = new()
        {
            [1] = new[] { Vector3.zero },
            [2] = new[] { new Vector3(-FormationSpacing, 0, 0), new Vector3(FormationSpacing, 0, 0) },
            [3] = new[] { new Vector3(0, 0, FormationSpacing), new Vector3(-FormationSpacing, 0, 0), new Vector3(FormationSpacing, 0, 0) },
            [4] = new[] { new Vector3(0, 0, FormationSpacing), new Vector3(FormationSpacing, 0, 0), new Vector3(-FormationSpacing, 0, 0), new Vector3(0, 0, -FormationSpacing) }
        };
        
        public CharacterService(
            ICharacterData data,
            ICharacterProvider provider,
            CharacterFactory factory,
            WeaponData weaponData,
            WeaponViewFactory weaponViewFactory,
            SquadPosition[] squadPositions)
        {
            _data = data;
            _provider = provider;
            _factory = factory;
            _weaponData = weaponData;
            _weaponViewFactory = weaponViewFactory;
            _squadPositions = squadPositions;

            Create();
        }
        
        public IReadOnlyList<Character> Characters => _characters;
        public IReadOnlyList<WeaponView> WeaponViews => _weaponViews;

        private void Create()
        {
            foreach (var (squadNumberType, type) in _provider.Characters)
            {
                if (type == CharacterType.None) 
                     continue;
                
                var squadPosition = _squadPositions.FirstOrDefault(squadPosition => squadPosition.Type == squadNumberType);
                
                var view = _factory.Create(_data.Characters[type], squadPosition.transform);
                var weaponView = _weaponViewFactory.Create(_weaponData.Weapons[view.WeaponType], view.Weapon, view.ShootPosition);
                _characters.Add(view);
                _weaponViews.Add(weaponView);
            }
            
            ApplyPosition();
        }

        private void ApplyPosition()
        {
            if (Offsets.TryGetValue(_characters.Count, out var offsets) == false)
                return;
            
            for (int i = 0; i < _characters.Count; i++)
                _characters[i].transform.localPosition = offsets[i];
        }
    }
}