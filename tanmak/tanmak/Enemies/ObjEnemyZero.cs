using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.TanmakuSequence;
using tanmak.CardActivateAnimate;
using tanmak.ProcessingBar;
using tanmak.CardNameShow;
using tanmak.Card;
using tanmak.Halo;
using System.Windows;
using tanmak.Servant;

namespace tanmak.Game
{
    public class ObjEnemyZero : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<Bulleter> bullets = new List<Bulleter>();
        List<int> Inter = new List<int>();
        List<int> Delay = new List<int>();
        DispatcherTimer dispancer;
        ObjPlayer player;

        public ObjEnemyZero(World world, ObjPlayer player) : base(world)
        {
            
            Width = 80;
            Height = 48;
            X = (World.Width - Width) / 2;
            Y = 100;
            MoveToRandom();
            Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            this.player = player;

            var C = new TimingCardObject(World, new SimpleCardName(World,"TESTING"),
                new ServantTestTenmakuSequence(World, this, player, 100),
                new UmbreonCardActivateAnimates(world), this, new BlueHalo(World, this));
            C.SetEndCall(MoveAway);
            C.Activate();

            
        }

        public override void OnUpdate()
        {
            foreach (GameObject obj in World.Objects)
            {
                if (!obj.Dead && obj is ObjOwnBullet)
                {
                    if (IsHit(this, obj))
                    {
                        ++HitCount;
                        player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                    }
                }
            }
        }
        DispatcherTimer timer;
        private void MoveToRandom()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            var Interval = 500;
            timer.Tick += delegate
            {
                double x = rand.NextDouble(40, World.Width - 50 - this.Width);

                double duration = 500; // Math.Abs(x - X) * 8;

                //timer.Interval = TimeSpan.FromMilliseconds(duration + Interval);

                MoveTo(x, rand.NextDouble(5, 25), duration);
            };

            timer.Start();
        }
        public void MoveAway()
        {
            if (timer.IsEnabled)
                timer.Stop();
            MoveTo(rand.NextDouble(20, World.Width - 20), -160, 500);
        }
    }
}
