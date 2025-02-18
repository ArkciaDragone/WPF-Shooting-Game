﻿using tanmak.Engine;

namespace tanmak.Game
{
    public abstract class Bulleter
    {
        public World World { get; set; }
        public GameObject Parent { get; set; }

        public Bulleter(GameObject parent)
        {
            Parent = parent;

            World = parent.World;
        }

        public abstract double Shoot();
    }
}
