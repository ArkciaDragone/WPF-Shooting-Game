using System.Windows.Media;
using tanmak.Engine;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class LinearBullet : EnemyBullet
    {
        double x_vec;
        double y_vec;
        double radius;
        public LinearBullet(EmptyBulletSkin skin,World world, double x, double y, double x_vec, double y_vec, double radius) : base(world)
        {
            X = x;
            Y = y;
            this.x_vec = x_vec;
            this.y_vec = y_vec;
            this.radius = radius;

            Width = skin.Width;
            Height = skin.Height;
            Damage = ScoreManager.NormalBulletDamage;
            Sprite = skin.GetSprit();
            //Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            X += x_vec;
            Y += y_vec;
            if (X >= -Width * 2 && X <= World.Width + Width * 2 && Y >= -Height * 2 && Y <= World.Height + Height * 2)
                return;

            double GetX(double tx, double ty)
            {
                tx -= X;
                ty -= Y;
                return tx * x_vec + ty * y_vec;
            }
            if (GetX(0, 0) > 0)
                return;
            if (GetX(0, World.Height) > 0)
                return;
            if (GetX(World.Width, 0) > 0)
                return;
            if (GetX(World.Width, World.Height) > 0)
                return;
            Dead = true;
        }
    }
}
