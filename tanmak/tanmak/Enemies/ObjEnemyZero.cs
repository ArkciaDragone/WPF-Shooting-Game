using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using tanmak.Engine;
using tanmak.BulletSkin;

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

            double av = 0.7, rv = 1.45;
            var I = new SimpleRedBigTamaSkin();
            var F = new SimpleColorfulBallSkin(11);
            var J = new SimpleYellowBigTamaSkin();
            var K = new SimpleIceBallSkin();
            bullets.Add(new ExposionBulleter(I, F, 100, 100, 200, 50,
                100, 2.5, 1.5, this));
            Inter.Add(10);
            Delay.Add(1);
            bullets.Add(new SplitBulleter(J, K, -100, 100, 200, 50, 10, 3, -10000, this));
            Inter.Add(10);
            Delay.Add(6);
            dispancer = new DispatcherTimer();
            int Tick = 0;
            dispancer.Interval = TimeSpan.FromMilliseconds(200);
            dispancer.Tick += delegate
            {
                ++Tick;
               for(int bulletIndex=0; bulletIndex<bullets.Count;++bulletIndex)
                    if(Delay[bulletIndex] < Tick)
                    if((Tick - Delay[bulletIndex]) % Inter[bulletIndex] == 0)
                        bullets[bulletIndex].Shoot();

                //dispancer.Interval = TimeSpan.FromMilliseconds(35000);
            };

            dispancer.Start();
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

        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
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

    }
}
