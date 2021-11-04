using System.Windows.Media;
using tanmak.Engine;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using tanmak.BulletSkin;
using System;

namespace tanmak.Game
{
    public class DirectedLinearBullet : EnemyBullet
    {
        double x_vec;
        double y_vec;
        double radius;
        public DirectedLinearBullet(EmptyBulletSkin skin,World world, double x, double y, double x_vec, double y_vec) : base(world)
        {
            X = x;
            Y = y;
            this.x_vec = x_vec;
            this.y_vec = y_vec;

            Damage = ScoreManager.NormalBulletDamage;
            Sprite = skin.GetSprit();
            ((ImageSprite)Sprite).SetAngle(Math.Atan2(y_vec, x_vec) );
            //Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            X += x_vec;
            Y += y_vec;

            ((ImageSprite)Sprite).SetAngle(Math.Atan2(y_vec, x_vec));
            CheckOutOfBounds();
        }
    }
}
