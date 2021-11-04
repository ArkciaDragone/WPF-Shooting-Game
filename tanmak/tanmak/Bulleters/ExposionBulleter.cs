using System;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class ExposionBulleter : Bulleter
    {
        Engine.Random Random = new Engine.Random();

        int dx, dy;
        int walktime, arrivetime, expways;

        double expspeed, expspeedrange;

        EmptyBulletSkin RSkin, bSkin;
        public ExposionBulleter(EmptyBulletSkin skina, EmptyBulletSkin skinb,
            int dx, int dy, int walktime, int arrivetime, 
            int exp_ways, double exp_speed , double exp_speed_range,GameObject parent) : base(parent)
        {
            expways = exp_ways;
            this.dx = dx;
            this.dy = dy;
            this.walktime = walktime;
            this.arrivetime = arrivetime;
            expspeed = exp_speed;
            expspeedrange = exp_speed_range;
            RSkin = skina;
            bSkin = skinb;
        }
        void DeadCall(double X, double Y)
        {
            X += RSkin.Width / 2;
            Y += RSkin.Height / 2;
            for(int wi=1;wi<= expways; ++wi)
            {
                double ang = Random.NextDouble(0, 360);
                double v = expspeed + Random.NextDouble(-expspeedrange, expspeedrange);

                double dx = v * Math.Cos(ang * Math.PI / 180);
                double dy = v * Math.Sin(ang * Math.PI / 180);
                World.AddObject(new LinearBullet(bSkin, World,
                    X, Y, dx, dy, 0));
            }
        }
        public override double Shoot()
        {
            double xs = Parent.X + Parent.Width / 2 - RSkin.Width / 2;
            double ys = Parent.Y + Parent.Height / 2 - RSkin.Width / 2;
            World.AddObject(new StoppingBullet(RSkin, World,
               xs, ys, xs + dx, ys + dy, walktime, arrivetime, DeadCall));
            return 0;
        }
    }
}
