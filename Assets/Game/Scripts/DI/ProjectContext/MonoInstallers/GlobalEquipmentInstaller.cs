using Game.Scripts.Configs;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Factory;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalEquipmentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<EquipmentData>()
                .AsSingle();
            
            Container
                .Bind<RarityEquipmentData>()
                .AsSingle();
            
            Container
                .Bind<RarityEquipmentViewFactory>()
                .AsSingle();
        }
    }
}