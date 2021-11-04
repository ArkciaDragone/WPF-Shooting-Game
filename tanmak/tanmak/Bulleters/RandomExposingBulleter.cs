using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class RandomExposingBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();
        int expways;

        double expspeed, expspeedrange;

        EmptyBulletSkin RSkin;
        public RandomExposingBulleter(EmptyBulletSkin skina,
            int exp_ways, double exp_speed , double exp_speed_range,GameObject parent,int exr=0) : base(parent)
        {
            expways = exp_ways;
            expspeed = exp_speed;
            expspeedrange = exp_speed_range;
            RSkin = skina;
            Random = new Engine.Random(exr);
        }
        public override double Shoot()
        {
            int wi;
            for(wi=0;wi<expways;++wi)
            {
                double ang = Random.NextDouble(0, 360);
                double v = expspeed + Random.NextDouble(-expspeedrange, expspeedrange);

                double dx = v * Math.Cos(ang * Math.PI / 180);
                double dy = v * Math.Sin(ang * Math.PI / 180);
                World.AddObject(new LinearBullet(RSkin, World,
                    Parent.X + Parent.Width/2, Parent.Y + Parent.Height/2, dx, dy, 0));
            }
            return 0;
        }
    }
}
