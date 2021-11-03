using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class RotateBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();
        int Ways;
        double Ang;
        double Spd;
        CircleBullet.TimeFunc rFunc;
        EmptyBulletSkin Skin;
        double aSpeed;
        public RotateBulleter(EmptyBulletSkin skin,int ways, double ang, double aspeed, double r, double rspeed,GameObject parent) : base(parent)
        {
            Skin = skin;
            Ways = ways;
            Ang = ang;
            double rF(int T)
            {
                return r + rspeed * T;
            }    
            rFunc += rF;
            aSpeed = aspeed;
        }

        public override double Shoot()
        {
            double count = Ways;

            double baseAngle = Ang;

            for (int i = 0; i < count; i++)
            {
                double angle = (360 / count) * i + baseAngle;
                angle = angle % 360;

                double angle_rad = angle / 180 * Math.PI;
                double aFunc(int Tick)
                {
                    return angle + Tick * aSpeed;
                }

                World.AddObject(new CircleBullet(Skin, World, Parent.X + Parent.Width/ 2 - Skin.Width / 2, Parent.Y + Parent.Height/2 - Skin.Height/2,
                    rFunc, aFunc));
            }

            return 0;
        }
    }
}
