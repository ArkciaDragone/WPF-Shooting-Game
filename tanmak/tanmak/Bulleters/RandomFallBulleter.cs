using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class RandomFallBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();
        double SpdRg;
        int Counts;
        double Spd;
        EmptyBulletSkin Skin;
        double RadRange;
        public RandomFallBulleter(EmptyBulletSkin skin, double Speed, double Speedrg,
            int Cnt, double RadRange, GameObject parent) : base(parent)
        {
            Counts = Cnt;
            Spd = Speed;
            SpdRg = Speedrg;
            Skin = skin;
            this.RadRange = RadRange;
        }

        public override double Shoot()
        {
            for(int wi=0;wi<Counts;++wi)
            {
                double sx, sy;
                sx = Random.NextDouble(World.Width / 6, World.Width * 5 / 6);
                sy = Random.NextDouble(-100, -40);
                double Rad = 90 + Random.NextDouble(-RadRange, RadRange);
                double Speed = Spd + Random.NextDouble(-SpdRg, SpdRg);
                double vx = Speed * Math.Cos(Rad * Math.PI / 180);
                double vy = Speed * Math.Sin(Rad * Math.PI / 180);
                World.AddObject(new LinearBullet(Skin, World, sx, sy, vx, vy, -1));
            }
            return 0;
        }
    }
}
