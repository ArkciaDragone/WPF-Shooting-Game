﻿using System;

namespace tanmak.Engine
{
    public class Random
    {
        System.Random rand = new System.Random((int)DateTime.UtcNow.TimeOfDay.TotalMilliseconds);
        public Random()
        {

        }
        public Random(int e)
        {
            rand = new System.Random((int)DateTime.UtcNow.TimeOfDay.TotalMilliseconds + e);
        }
        public double NextDouble()
        {
            return rand.NextDouble();
        }

        public double NextDouble(double min, double max)
        {
            return (double)rand.Next((int)(min * 10000), (int)(max * 10000)) / 10000;
        }

        public int Next(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
