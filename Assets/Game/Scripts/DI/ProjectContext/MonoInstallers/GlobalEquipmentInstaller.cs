using Game.Scripts.Equipment.EquipmentContext.Provider;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalEquipmentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EquipmentProvider>()
                .AsSingle();
        }
    }
}