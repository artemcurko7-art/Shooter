using Game.Scripts.UI.TabContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class TabInstaller : MonoInstaller
    {
        [SerializeField] private TabView[] _views;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Tab>()
                .AsSingle()
                .WithArguments(_views);
        }
    }
}