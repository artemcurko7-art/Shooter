using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Missions
{
    [CreateAssetMenu(fileName = "TaskData", menuName = "Data/TaskData")]
    public class TasksData : ScriptableObject
    {
        public List<Task> Tasks = new();

        [Serializable]
        public class Task
        {
            [Range(1, 3)]
            public int difficulty;
            public LocalizedText TaskTextTranslations;
            public string type;
            public int reward;
            public int quota;
            public Sprite rewardIcon;

            [Serializable]
            public class LocalizedText
            {
                public string Ru;
                public string En;
                public string Tr;
            }

            public string GetLocalizedTask(string languageCode)
            {
                return languageCode switch
                {
                    "ru" => TaskTextTranslations.Ru,
                    "en" => TaskTextTranslations.En,
                    "tr" => TaskTextTranslations.Tr,
                    _ => TaskTextTranslations.En,
                };
            }
        }
    }
}