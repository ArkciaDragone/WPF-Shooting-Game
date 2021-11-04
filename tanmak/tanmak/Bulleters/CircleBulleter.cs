using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class CircleBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();
        int Ways;
        double Ang;
        double Spd;
        EmptyBulletSkin Skin;
        public CircleBulleter(EmptyBulletSkin skin,int ways, double ang, double speed,GameObject parent) : base(parent)
        {
            Ways = ways;
            Ang = ang;
            Spd = speed;
            Skin = skin;
        }

        public override double Shoot()
        {
            double count = Ways;

            double baseAngle = Ang;
            if(baseAngle < -9999)
                   baseAngle = Random.NextDouble(0, 360);

            for (int i = 0; i < count; i++)
            {
                double angle = (360 / count) * i + baseAngle;
                angle = angle % 360;

                double angle_rad = angle / 180 * Math.PI;
                double speed = Spd ;
                double xvec = Math.Cos(angle_rad) * speed;
                double yvec = Math.Sin(angle_rad) * speed;

                World.AddObject(new LinearBullet(Skin, World, Parent.X + Parent.Width/ 2 - Skin.Width / 2, Parent.Y + Parent.Height/ 2 - Skin.Height / 2, xvec, yvec, 3));
            }

            return 0;
        }
    }
}
