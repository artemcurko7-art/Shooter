using System.Collections.Generic;

namespace Game.Scripts.Mathematics
{
    public static class Shuffler
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int index = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[index]) = (list[index], list[i]);
            }
        }
    }
}