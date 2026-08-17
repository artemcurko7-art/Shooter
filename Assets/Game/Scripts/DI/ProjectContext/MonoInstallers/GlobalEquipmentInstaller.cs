using Game.Scripts.Configs;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Factory;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.ProjectContext.MonoInstallers
{
    public class GlobalEquipmentInstaller : MonoInstaller
    {
        [SerializeField] private Slot _slot;
        
        public override void InstallBindings()
        {
            Container
                .Bind<EquipmentData>()
                .AsSingle();
            
            Container
                .Bind<RarityEquipmentData>()
                .AsSingle();
            
            Container
                .Bind<SlotFactory>()
                .AsSingle()
                .WithArguments(_slot);
        }
    }
}