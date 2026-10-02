using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Replacement;
using Game.Scripts.Equipment.CharacterContext.Repository;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.SquadContext.Type;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class CharacterEquipmentInstaller : MonoInstaller
    {
        [SerializeField] private CharacterSlot _slot;
        [SerializeField] private CharacterDropSlot[] _dropSlots;
        [SerializeField] private DisplayCharacterReplacement _displayReplacement;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private Transform _container;
        
        public override void InstallBindings()
        {
            Bind();
            BindRepository();
            BindReplacement();
        }

        private void Bind()
        {
            Container
                .Bind<CharacterData>()
                .AsSingle();
            
            Container
                .Bind<CharacterSlotRewardService>()
                .AsSingle()
                .WithArguments(_container);
            
            Container
                .Bind<CharacterSlotFactory>()
                .AsSingle()
                .WithArguments(_slot);

            Container
                .BindInterfacesAndSelfTo<CharacterSlotHandler>()
                .AsSingle();
            
            Container
                .Bind<CharacterDropSlot[]>()
                .FromInstance(_dropSlots)
                .AsSingle();
        }

        private void BindRepository()
        {
            Container
                .Bind<SlotRepository<CharacterSlot>>()
                .AsSingle();
            
            Container
                .Bind<FreeSlotRegistry<SquadNumberType, CharacterSlot>>()
                .AsSingle();
        }   

        private void BindReplacement()
        {
            Container
                .BindInterfacesTo<CharacterReplacementController>()
                .AsCached();

            Container
                .Bind<ISubscriber>()
                .To<CharacterReplacementTabOpened>()
                .AsCached()
                .WithArguments(_displayReplacement, _canvas, _gridLayoutGroup);
        }
    }
}