﻿using System;
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
            this.player = player;
            string[] efn = new string[13];
            for (int wi = 0; wi < 13; ++wi)
                efn[wi] = "Sources/Ens/" + (wi + 1).ToString() + ".png";
            string[] fn = { "Sources/Comey.ico" };
            Sprite = new ImageSprite(efn, 60, animationWait:1);

            Width = ((ImageSprite)Sprite).GetWidth();
            Height = ((ImageSprite)Sprite).GetHeight();
            //MoveTo(World.Width / 2 - Width / 2, World.Height / 3 - Height / 2, 0.2);
            MoveToRandom();
            //Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);
            var a = new NormalCardObject(World,new SimpleCardName(World, "『伊布』布伊"),
                new SunriseTenshiTanmakuSequence(World, this, Times:3), new EeveeCardActivateAnimate(World),
                this, 10);
            var b = new TimingCardObject(World, new SimpleCardName(World, "『伊布』布伊2"),
                new SunriseTenshiTanmakuSequence(World, this, Times:3), new EeveeCardActivateAnimate(World),
                this);
            a.SetEndCall(b.Activate);
            a.Activate();
            return;

            var fst = new EeveeCardActivateAnimate(World);

            var cnm = new SimpleCardName(World, "『伊布』布伊");
            EmptyTanmakuSequence emp = new SunriseTenshiTanmakuSequence(world, this, Times:2);
            EmptyTanmakuSequence nex = new ExposionAroundTS(world, this);
            SimpleTimingBar smba = new SimpleTimingBar(World, emp.EndTick + nex.EndTick);
            fst.SetEndCall(delegate { emp.Activate(); smba.Activate(); });
            emp.SetEndCall(nex.Activate);
            nex.SetEndCall(delegate { cnm.Stop(); });



            cnm.Activate();
            fst.Activate();

            return;

            

            double av = 0.15, rv = 1.2;
            var I = new SimpleYellowBigTamaSkin();
            var R = new SimpleRedBigTamaSkin();
            var E = new SimpleIceBallSkin();

            var Ways = 30;
            var Waves = 5;//*2
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
                        HitCount++;
                        player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                        obj.Dead = true;
                    }
                }
            }
        }

        private void MoveToRandom()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
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
