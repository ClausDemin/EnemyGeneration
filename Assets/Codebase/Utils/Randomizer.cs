using System;
using UnityEngine;

namespace Assets.Codebase.Utils
{
    public static class Randomizer
    {
        private static System.Random s_random = new System.Random();

        public static int GetRandomInt(int minValue = int.MinValue, int maxValue = int.MinValue) 
        { 
            return s_random.Next(minValue, maxValue);
        }

        public static float GetRandomFloat(float minValue = 0, float maxValue = 1)
        {
            return (float)s_random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int GetRandomSign(float positiveChance = 0.5f)
        {
            if (GetRandomFloat() < positiveChance)
            {
                return -1;
            }

            return 1;
        }

        public static Vector3 GetRandomVector() 
        { 
            return 
                new Vector3(GetRandomSign() * GetRandomFloat(), GetRandomSign() * GetRandomFloat(), GetRandomSign() * GetRandomFloat())
                .normalized;
        }
    }
}