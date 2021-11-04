using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using tanmak.Engine;
using tanmak.BulletSkin;

namespace tanmak.Game
{
    public class TenshiInSunriseCountry : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<Bulleter> bullets = new List<Bulleter>();
        List<int> Inter = new List<int>();
        List<int> Delay = new List<int>();
        DispatcherTimer dispancer;
        ObjPlayer player;

        public TenshiInSunriseCountry(World world, ObjPlayer player) : base(world)
        {
            string[] fn = { "Sources/Comey.ico" };
            Sprite = new ImageSprite(fn, 100);

            Width = ((ImageSprite)Sprite).GetWidth();
            Height = ((ImageSprite)Sprite).GetHeight();
            //MoveTo(World.Width / 2 - Width / 2, World.Height / 3 - Height / 2, 0.2);
            MoveToRandom();
            //Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);


            this.player = player;

            double av = 0.19, rv = 2.1;
            var I = new SimpleYellowBigTamaSkin();
            var R = new SimpleRedBigTamaSkin();
            var E = new SimpleIceBall();

            var Ways = 25;
            var Waves = 3;//*2
            var Wave_Inter = 2;
            var Global_Inter = 15 + Wave_Inter * 2 * (Waves);
            var Starting_Delay = 5;

            var ms_per_tic = 100;

            for(int wi=0;wi<Waves;++wi)
            {
                bullets.Add(new RotateBulleter(I, Ways, 0, av, 0, rv, this));
                Delay.Add(Starting_Delay + (wi * 2 * Wave_Inter));
                Inter.Add(Global_Inter);

                bullets.Add(new RotateBulleter(R, Ways, 0, -av, 0, rv, this));
                Delay.Add(Starting_Delay + (wi * 2 + 1) * Wave_Inter);
                Inter.Add(Global_Inter);
            }
            int Ws = 10;
            bullets.Add(new CircleBulleter(E, Ws, -10000, 2, this));
            Delay.Add(Starting_Delay);
            Inter.Add(Wave_Inter * 8 - 2);


            dispancer = new DispatcherTimer();
            int Tick = 0;
            dispancer.Interval = TimeSpan.FromMilliseconds(ms_per_tic);
            dispancer.Tick += delegate
            {
                ++Tick;
               for(int bulletIndex=0; bulletIndex<bullets.Count;++bulletIndex)
                    if(Delay[bulletIndex] <= Tick)
                    if((Tick - Delay[bulletIndex]) % Inter[bulletIndex] == 0)
                        bullets[bulletIndex].Shoot();

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
                        player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                        obj.Dead = true;
                    }
                }
            }
        }

        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            var Interval = 500;
            timer.Tick += delegate
            {
                double x = rand.NextDouble(40, World.Width - 50 - this.Width);

                double duration = 300; // Math.Abs(x - X) * 8;

                timer.Interval = TimeSpan.FromMilliseconds(duration + Interval);

                MoveTo(x, rand.NextDouble(5, 25), duration);
            };

            timer.Start();
        }

    }
}
