using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.SquadContext;
using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public class CharacterService
    {
        private readonly CharacterFactory _factory;
        private readonly ICharacterProvider _provider;
        private readonly SquadPosition[] _squadPositions;
        private readonly List<Character> _characters = new();
        private readonly Dictionary<int, Vector3[]> Offsets = new()
        {
            [1] = new[] { Vector3.zero },
            [2] = new[] { new Vector3(-2, 0, 0), new Vector3(2, 0, 0) },
            [3] = new[] { new Vector3(0, 0, 2), new Vector3(-2, 0, 0), new Vector3(2, 0, 0) },
            [4] = new[] { new Vector3(0, 0, 2), new Vector3(2, 0, 0), new Vector3(-2, 0, 0), new Vector3(0, 0, -2) }
        };
        
        public CharacterService(CharacterFactory factory, ICharacterProvider provider, SquadPosition[] squadPositions)
        {
            _factory = factory;
            _provider = provider;
            _squadPositions = squadPositions;

            Create();
        }


        private void Create()
        {
            foreach (var (type, character) in _provider.Characters)
            {
                if (character == null) 
                    continue;
                
                var squadPosition = _squadPositions.FirstOrDefault(squadPosition => squadPosition.Type == type);

                var view = _factory.Create(character, squadPosition.transform);
                _characters.Add(view);
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