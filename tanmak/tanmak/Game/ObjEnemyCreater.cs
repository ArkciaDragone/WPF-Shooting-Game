﻿using tanmak.Engine;

namespace tanmak.Game
{
    public class ObjEnemyCreater : GameObject
    {
        public ObjEnemyCreater(World world, ObjPlayer player) : base(world)
        {
            world.AddObject(new Eevees(world, player));
        }
    }
}
