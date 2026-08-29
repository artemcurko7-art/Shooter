using System.Collections.Generic;

namespace Game.Scripts.UserUtils
{
    public static class Shuffler
    {
        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int index = NumberGeneration.GetRandom(0, i);
                (list[i], list[index]) = (list[index], list[i]);
            }
            
            return list;
        }
    }
}