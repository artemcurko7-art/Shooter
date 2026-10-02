using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.SquadContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private SquadPosition[] _squadPositions;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CharacterService>()
                .AsSingle()
                .WithArguments(_squadPositions)
                .NonLazy();

            Container
                .Bind<CharacterFactory>()
                .AsSingle();
        }
    }
}