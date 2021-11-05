using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class CircleCenteringBulleters : Bulleter
    {
        static Engine.Random Random = new Engine.Random();
        int Ways;
        double InitR, MidR;
        int Len1, Len2, Len3;
        double dAngle;
        EmptyBulletSkin Skin;
        public CircleCenteringBulleters(
            EmptyBulletSkin skin,int ways,
            double initR, double midR,
            int Ticks1,int Ticks3,
            double dAngle,GameObject parent) : base(parent)
        {
            Ways = ways;
            Skin = skin;
            InitR = initR;
            MidR = midR;
            Len1 = Ticks1;
            Len3 = Ticks3;
            this.dAngle = dAngle;
        }

        public override double Shoot()
        {
            Shoot(Random.NextDouble(0, Math.PI * 2), 100);
            return 0;
        }
        public double Shoot(double nAng, int Tick2)
        {
            int ni = Tick2;
            double RFunc(int Ticks)
            {
                if (Ticks <= Len1)
                    return InitR + (MidR - InitR) * Math.Sqrt(Ticks * 1.0 / Len1);
                Ticks -= Len1;
                if (Ticks <= ni)
                    return MidR;
                Ticks -= ni;
                if (Ticks <= Len3)
                    return MidR - MidR * Math.Pow(Ticks * 1.0 / Len3, 3);
                else
                    throw new Exception("????");
            }
            int wi;
            for (wi = 1; wi <= Ways; ++wi) 
            {
                double Ang = Math.PI * 2 * wi / Ways;
                double Ex = Ang - nAng;
                Ex %= (Math.PI * 2);
                if (Math.Abs(Ex) <= this.dAngle)
                    continue;
                World.AddObject(new CenteringBullet(Skin, World, Ang, RFunc));
            }
            return 0;
        }
    }
}
