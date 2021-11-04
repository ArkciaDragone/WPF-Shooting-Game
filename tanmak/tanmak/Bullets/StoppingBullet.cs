using System.Windows.Media;
using tanmak.Engine;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using tanmak.BulletSkin;
using System;

namespace tanmak.Game
{
    public class StoppingBullet : EnemyBullet
    {
        public delegate void CALLBACK(double nx,double ny);
        double xp, yp;
        int walktick, arrivetick;
        double xd, yd;
        CALLBACK DeathCall;
        int Tick;
        public StoppingBullet(EmptyBulletSkin skin,World world, 
            double x, double y, double xd, double yd, int walkticks,
            int arriveticks, CALLBACK cb = null) : base(world)
        {
            X = x;
            Y = y;
            xp = X;yp = y;
            this.xd = xd;
            this.yd = yd;
            DeathCall = cb;
            Tick = 0;
            walktick = walkticks;
            arrivetick = arriveticks;

            Damage = ScoreManager.NormalBulletDamage;
            Sprite = skin.GetSprit();
            //Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            Tick++;
            if(Tick<=walktick)
            {
                //0:0  1:1   2-2/(x+1)
                double px = (Tick * 1.0 / walktick);
                double py = Math.Pow((px - 1), 3) + 1;
                X = xp + (xd - xp) * py;
                Y = yp + (yd - yp) * py;
            }
            if(Tick == walktick + arrivetick)
            {
                DeathCall?.Invoke(X, Y);
                this.Dead = true;
            }
        }
    }
}
