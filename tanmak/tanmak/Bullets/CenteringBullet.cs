using System.Windows.Media;
using tanmak.Engine;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using tanmak.BulletSkin;
using System;

namespace tanmak.Game
{
    public class CenteringBullet : EnemyBullet
    {

        public delegate double RFUNC(int Ticks);
        RFUNC RFunc;
        double sAng,cAng;
        int Tick = 0;
        public CenteringBullet(EmptyBulletSkin skin,World world,
            double Ang, RFUNC rFunc) : base(world)
        {

            Width = skin.Width;
            Height = skin.Height;
            Damage = ScoreManager.NormalBulletDamage;
            Sprite = skin.GetSprit();
            this.RFunc = rFunc;

            sAng = Math.Sin(Ang);
            cAng = Math.Cos(Ang);
            double r = rFunc(0);
            X = r * sAng;
            Y = r * cAng;
            
            //Sprite = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), radius);
        }

        public override void OnUpdate()
        {
            double r = RFunc(Tick);


            X = r * sAng;
            Y = r * cAng;
            if(Math.Abs(r) <  0.01)
                Dead = true;
            ++Tick;
        }
    }
}
