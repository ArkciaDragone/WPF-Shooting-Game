using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class NullBulleter : Bulleter
    {
        EmptyBulletSkin Skin;
        tanmak.Game.EnemyBullet eB;
        public NullBulleter(tanmak.Game.EnemyBullet b,GameObject parent) : base(parent)
        {
            eB = b;
        }

        public override double Shoot()
        {
            World.AddObject(eB);
            return 0;
        }
    }
}
