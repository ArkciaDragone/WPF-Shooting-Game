using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class RandomWallBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();
        double Spd;
        EmptyBulletSkin Skin;
        double RRange;
        double SplitSize;
        public RandomWallBulleter(EmptyBulletSkin skin, double Speed, 
            double RRange,double splitsize, GameObject parent) : base(parent)
        {
            Spd = Speed;
            Skin = skin;
            this.RRange = RRange;
            this.SplitSize = splitsize;
        }

        public override double Shoot()
        {
            int Num = (int)(World.Width * 1.0 / (Skin.Width*1.5) + 1) + 1;
            double pos = 0.5 + Random.NextDouble(-RRange / 2, RRange / 2);
            for(int wi=0; wi<Num;++wi)
            {
                double py = -30;
                double px = wi * Skin.Width*1.5 - Skin.Width / 2;
                if (Math.Abs(px / World.Width - pos) <= SplitSize)
                    continue;
                else
                    World.AddObject(new LinearBullet(Skin, World, px, py,
                        0, Spd, -1));
            }
            return 0;
        }
    }
}
