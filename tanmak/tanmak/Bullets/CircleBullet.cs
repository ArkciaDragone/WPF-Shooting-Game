using System.Windows.Media;
using tanmak.Engine;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class CircleBullet : EnemyBullet
    {
        public delegate double TimeFunc(int tp);
        TimeFunc rFunc, aFunc;
        double xpro, ypro;
        int Tick = 0;
        public CircleBullet(EmptyBulletSkin skin,World world, double x,double y,TimeFunc r, TimeFunc ang) : base(world)
        {
            xpro = x;
            ypro = y;
            X = x;
            Y = y;
            rFunc = r;
            aFunc = ang;

            Damage = ScoreManager.NormalBulletDamage;
            Sprite = skin.GetSprit();
            //Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            ++Tick;
            double r = rFunc(Tick);
            double a = aFunc(Tick);
            double angle = a % 360;

            double angle_rad = angle / 180 * Math.PI;
            X = Math.Cos(angle_rad) * r + xpro;
            Y = Math.Sin(angle_rad) * r + ypro;
            if (r > World.Height)
                Dead = true;
        }
    }
}
