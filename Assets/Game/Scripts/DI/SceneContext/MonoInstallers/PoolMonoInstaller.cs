using System;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.MapContext;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.Service.Map;
using Game.Scripts.Service.UnitContext;
using Game.Scripts.TextPopup;
using Game.Scripts.VFX.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PoolMonoInstaller : MonoInstaller
    {
        // private ICharacterService _characterService;
        //
        // [Inject]
        // public void Construct(ICharacterService characterService)
        // {
        //     _characterService = characterService;
        // }
        
        public override void InstallBindings()
        {
            BindUnit();
            BindMap();
            BindBullet();
            //BindPassiveAbilityView();
            BindEffect();
            BindDamageTextPopup();
        }

        private void BindUnit()
        {
            foreach (var type in Enum.GetValues(typeof(UnitType)))
            {
                if ((UnitType)type == UnitType.None)
                    continue;
                
                Container
                    .Bind<UnitPool>()
                    .AsCached()
                    .WithArguments((UnitType)type);
            }
            
            Container
                .Bind<UnitFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UnitService>() // add bind interfaces to
                .AsSingle();
        }

        private void BindMap()
        {
            Container
                .Bind<MapPool>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MapService>()
                .AsSingle();
        }
        
        private void BindBullet()
        {
            Container
                .Bind<ProjectilePool>()
                .AsSingle();
        }

        // private void BindPassiveAbilityView()
        // {
        //     foreach (var character in _characterService.Characters)
        //     {
        //         Container
        //             .Bind<PassiveAbilityViewPool>()
        //             .AsSingle()
        //             .WithArguments(character.PassiveAbilityData, character.t);;
        //     }
        // }
        
        private void BindEffect()
        {
            foreach (var type in Enum.GetValues(typeof(EffectType)))
            {
                if ((EffectType)type == EffectType.None)
                    continue;
                
                Container
                    .Bind<EffectPool>()
                    .AsCached()
                    .WithArguments((EffectType)type);;
            }

            Container
                .Bind<IEffectPoolRepository>()
                .To<EffectPoolRepository>()
                .AsSingle();
        }

        private void BindDamageTextPopup()
        {
            Container
                .Bind<DamageTextPopupPool>()
                .AsSingle();
        }
    }
}