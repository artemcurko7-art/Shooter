using Game.Scripts.Configs;
using Game.Scripts.GameWorldContext;
using Game.Scripts.GameWorldContext.Data;
using Game.Scripts.GameWorldContext.Provider;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class GameWorldInstaller : MonoInstaller
    {
        private IGameWorldData _data;
        private IGameWorldProvider _provider;
        
        [Inject]
        public void Construct(IGameWorldData data, IGameWorldProvider provider)
        {
            _data = data;
            _provider = provider;
        }
        
        public override void InstallBindings()
        {
            Container
                .Bind<GameWorld>()
                .AsSingle();
            
            Container
                .Bind<GameWorldConfig>()
                .FromInstance(_data.Worlds[_provider.Type])
                .AsSingle();
        }
    }
}