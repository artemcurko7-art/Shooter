using System;
using Game.Scripts.Service.Equipment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment
{
    public class ContainerHandlerTransform : MonoBehaviour
    {
        private EquipmentService _equipmentService;
        private SortingEquipmentByParameters _sorting;
        private bool _isSorting;
        
        [Inject]
        public void Construct(EquipmentService equipmentService, SortingEquipmentByParameters sorting)
        {
            _equipmentService = equipmentService;
            _sorting = sorting;
        }
        
        public void OnTransformChildrenChanged()
        {
            if (_equipmentService == null || _isSorting)
                return;
            
            _equipmentService._slots.Clear();;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                _equipmentService._slots.Add(transform.GetChild(i).GetComponent<Slot>());
            }
            
            SortEquipment();
        }

        private void SortEquipment()
        {
            _isSorting = true;
            
            try
            {
                _sorting.Sort(_equipmentService._slots);
            }
            finally
            {
                _isSorting = false;
            }
        }
    }
}