using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Static.Requirements;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Type;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
    [CustomEditor(typeof(UnitConfig))]
    public sealed class UnitConfigEditor : UnityEditor.Editor
    {
        private SerializedProperty _attackerDatesProp;
        private SerializedProperty _commonStatsProp;

        private void OnEnable()
        {
            _attackerDatesProp = serializedObject.FindProperty(UnitConfig.AttackerDatesFieldName);
            _commonStatsProp = serializedObject.FindProperty(UnitConfig.StatDatesFieldName);
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawPropertiesExcluding(serializedObject, UnitConfig.AttackerDatesFieldName, UnitConfig.StatDatesFieldName);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Common Stats", EditorStyles.boldLabel);
            EnsureCommonStatsIsValid();
            DrawCommonStats();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Attackers Configuration", EditorStyles.boldLabel);
            EnsureAttackerDatesIsValid();
            DrawAttackerDates();

            serializedObject.ApplyModifiedProperties();
        }

        private void EnsureCommonStatsIsValid()
        {
            if (_commonStatsProp == null)
                return;
            
            StatType[] defaultStats = { StatType.Health, StatType.Defence, StatType.Speed, StatType.Distance };
            
            bool isValid = true;
    
            if (_commonStatsProp.arraySize != defaultStats.Length)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < defaultStats.Length; i++)
                {
                    var element = _commonStatsProp.GetArrayElementAtIndex(i);
                    var typeProp = element.FindPropertyRelative(UnitStatData.TypeFieldName);
            
                    if (typeProp == null || typeProp.enumValueIndex != (int)defaultStats[i])
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid == false)
            {
                _commonStatsProp.ClearArray();
        
                for (int i = 0; i < defaultStats.Length; i++)
                {
                    _commonStatsProp.InsertArrayElementAtIndex(i);
                    
                    var element = _commonStatsProp.GetArrayElementAtIndex(i);
            
                    var typeProp = element.FindPropertyRelative(UnitStatData.TypeFieldName);
                    
                    if (typeProp != null)
                        typeProp.enumValueIndex = (int)defaultStats[i];
            
                    var valueProp = element.FindPropertyRelative(UnitStatData.ValueFieldName);
                    
                    if (valueProp != null)
                        valueProp.intValue = 0;
                }
                
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update(); 
            }
        }

        private void DrawCommonStats()
        {
            if (_commonStatsProp == null) return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Type", EditorStyles.miniBoldLabel, GUILayout.Width(100));
            EditorGUILayout.LabelField("Value", EditorStyles.miniBoldLabel);
            EditorGUILayout.EndHorizontal();
            
            for (int i = 0; i < _commonStatsProp.arraySize; i++)
            {
                var element = _commonStatsProp.GetArrayElementAtIndex(i);
                var typeProp = element.FindPropertyRelative(UnitStatData.TypeFieldName);
                var valueProp = element.FindPropertyRelative(UnitStatData.ValueFieldName);

                EditorGUILayout.BeginHorizontal();

                if (typeProp != null)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.EnumPopup((StatType)typeProp.enumValueIndex, GUILayout.Width(100));
                    }
                }

                if (valueProp != null)
                {
                    EditorGUILayout.PropertyField(valueProp, GUIContent.none);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        private void EnsureAttackerDatesIsValid()
        {
            var config = (UnitConfig)target;
            
            var requiredStates = Enum.GetValues(typeof(StateType))
                .Cast<StateType>()
                .Where(type => type != StateType.None)
                .ToArray();

            bool isValid = true;
            
            if (config.AttackerDates == null || config.AttackerDates.Length != requiredStates.Length)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < requiredStates.Length; i++)
                {
                    if (config.AttackerDates[i].StateType != requiredStates[i])
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid == false)
            {
                _attackerDatesProp.ClearArray();
                
                for (int i = 0; i < requiredStates.Length; i++)
                {
                    _attackerDatesProp.InsertArrayElementAtIndex(i);
                    
                    var element = _attackerDatesProp.GetArrayElementAtIndex(i);
                    
                    var stateTypeProp = element.FindPropertyRelative(UnitAttackerData.StateTypeFieldName);
                    
                    if (stateTypeProp != null)
                        stateTypeProp.enumValueIndex = (int)requiredStates[i];

                    var attackersProp = element.FindPropertyRelative(UnitAttackerData.AttackersContextFieldName);
                    
                    if (attackersProp != null)
                        attackersProp.ClearArray();
                }
                
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawAttackerDates()
        {
            if (_attackerDatesProp == null)
                return;

            for (int i = 0; i < _attackerDatesProp.arraySize; i++)
            {
                var element = _attackerDatesProp.GetArrayElementAtIndex(i);
                var stateTypeProp = element.FindPropertyRelative(UnitAttackerData.StateTypeFieldName);
                var attackersProp = element.FindPropertyRelative(UnitAttackerData.AttackersContextFieldName);

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                if (stateTypeProp != null)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.EnumPopup("State type", (StateType)stateTypeProp.enumValueIndex);
                    }
                }
                
                if (attackersProp != null)
                {
                    EditorGUI.BeginChangeCheck();
                    int newSize = EditorGUILayout.IntField("Attacker Count", attackersProp.arraySize);
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        int oldSize = attackersProp.arraySize;
                        attackersProp.arraySize = newSize;

                        // ✅ Если мы добавили новые элементы, сбрасываем их в None
                        if (newSize > oldSize)
                        {
                            for (int k = oldSize; k < newSize; k++)
                            {
                                var newContextElement = attackersProp.GetArrayElementAtIndex(k);
                                
                                var newTypeProp = newContextElement.FindPropertyRelative(UnitAttackerContext.TypeFieldName);
                                if (newTypeProp != null) newTypeProp.enumValueIndex = 0; // None

                                var newStatsProp = newContextElement.FindPropertyRelative(UnitAttackerContext.StatDatesFieldName);
                                if (newStatsProp != null) newStatsProp.ClearArray();
                            }
                        }
                    }

                    // ✅ Собираем уже выбранные типы (исключая None)
                    var usedTypes = new List<UnitAttackerType>();
                    for (int k = 0; k < attackersProp.arraySize; k++)
                    {
                        var ctxElement = attackersProp.GetArrayElementAtIndex(k);
                        var tProp = ctxElement.FindPropertyRelative(UnitAttackerContext.TypeFieldName);
                        if (tProp != null) 
                        {
                            var type = (UnitAttackerType)tProp.enumValueIndex;
                            if (type != UnitAttackerType.None)
                                usedTypes.Add(type);
                        }
                    }

                    for (int j = 0; j < attackersProp.arraySize; j++)
                    {
                        var contextElement = attackersProp.GetArrayElementAtIndex(j);
                        var typeProp = contextElement.FindPropertyRelative(UnitAttackerContext.TypeFieldName);
                        var statsProp = contextElement.FindPropertyRelative(UnitAttackerContext.StatDatesFieldName);

                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                        if (typeProp != null)
                        {
                            var currentType = (UnitAttackerType)typeProp.enumValueIndex;
                            
                            EditorGUI.BeginChangeCheck();
                            EditorGUILayout.PropertyField(typeProp, new GUIContent("Attacker Type"));
                            
                            if (EditorGUI.EndChangeCheck())
                            {
                                serializedObject.ApplyModifiedProperties();
                                
                                // ✅ Если выбрали None — очищаем статы, иначе — создаем новые
                                if ((UnitAttackerType)typeProp.enumValueIndex == UnitAttackerType.None)
                                    statsProp?.ClearArray();
                                else
                                    UpdateStatsIfNeeded(statsProp, (UnitAttackerType)typeProp.enumValueIndex);
                            }

                            // ✅ Вывод предупреждения о дубликате
                            if (currentType != UnitAttackerType.None && usedTypes.Count(t => t == currentType) > 1)
                            {
                                EditorGUILayout.HelpBox($"Тип {currentType} уже выбран для другого элемента! Выберите другой тип.", MessageType.Error);
                            }
                        }
                        
                        if (statsProp != null)
                        {
                            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Type", EditorStyles.miniBoldLabel, GUILayout.Width(100));
                            EditorGUILayout.LabelField("Value", EditorStyles.miniBoldLabel);
                            EditorGUILayout.EndHorizontal();

                            for (int k = 0; k < statsProp.arraySize; k++)
                            {
                                var statElement = statsProp.GetArrayElementAtIndex(k);
                                var statTypeProp = statElement.FindPropertyRelative(UnitStatData.TypeFieldName);
                                var statValueProp = statElement.FindPropertyRelative(UnitStatData.ValueFieldName);

                                EditorGUILayout.BeginHorizontal();
   
                                if (statTypeProp != null)
                                {
                                    using (new EditorGUI.DisabledScope(true))
                                    {
                                        EditorGUILayout.EnumPopup((StatType)statTypeProp.enumValueIndex, GUILayout.Width(100));
                                    }
                                }

                                if (statValueProp != null)
                                {
                                    EditorGUILayout.PropertyField(statValueProp, GUIContent.none);
                                }

                                EditorGUILayout.EndHorizontal();
                            }
                        }

                        EditorGUILayout.EndVertical();
                    }
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }

        private void UpdateStatsIfNeeded(SerializedProperty statsProp, UnitAttackerType selectedType)
        {
            if (statsProp == null)
                return;
            
            if (UnitAttackerRequirements.Requirements.TryGetValue(selectedType, out var requiredStats) == false)
                return;
            
            statsProp.ClearArray();
            
            for (int i = 0; i < requiredStats.Length; i++)
            {
                statsProp.InsertArrayElementAtIndex(i);
                
                var element = statsProp.GetArrayElementAtIndex(i);
                
                var typeProp = element.FindPropertyRelative(UnitStatData.TypeFieldName);
                
                if (typeProp != null)
                    typeProp.enumValueIndex = (int)requiredStats[i];
                
                var valueProp = element.FindPropertyRelative(UnitStatData.ValueFieldName);
                if (valueProp != null)
                    valueProp.intValue = 0;
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}