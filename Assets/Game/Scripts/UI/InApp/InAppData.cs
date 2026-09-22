using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.InApp
{
    [CreateAssetMenu(fileName = "InAppGroupData", menuName = "Data/InAppGroupData")]
    public class InAppData : ScriptableObject
    {
        public List<InAppGroup> Groups = new();

        [Serializable]
        public class InAppGroup
        {
            public List<InApp> InApps = new();

            [Serializable]
            public class InApp
            {
                public Sprite icon;
                public Sprite frame;
                public int count;

                [Serializable]
                public class LocalizedText
                {
                    public string Ru;
                    public string En;
                    public string Tr;
                }
            }
        }
    }
}