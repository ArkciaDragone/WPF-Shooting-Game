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
            Sprite = new ImageSprite(efn, 60, animationWait: 1);

            Width = ((ImageSprite)Sprite).GetWidth();
            Height = ((ImageSprite)Sprite).GetHeight();
            MoveTo(World.Width / 2 - Width / 2, World.Height / 5 - Height / 2, 0.2);
            MoveToRandom();
            //Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            var a = new NormalCardObject(World, new SimpleCardName(World, "『瞬符』高速星星"),
                new HighSpeedStarTanmakuSequence(World, this, Times: 999999), new EeveeCardActivateAnimate(World),
                this, new WhiteHalo(World, this), 100);
            //a.Activate();

            var b = new NormalCardObject(World, new SimpleCardName(World, "『冻风』冰冻之风"),
                new FreezingWindTanmakuSequence(World, this, Times: 999999), new GlaceonCardActivateAnimate(World),
                this, new LightBlueHalo(World, this), 100);
            //b.Activate();

            var c = new NormalCardObject(World, new SimpleCardName(World, "『恶符』恶之波动"),
                new EvilWaveTanmakuSequence(World, this, Times: 999999), new UmbreonCardActivateAnimates(World),
                this, new EmptyHalo(World, this), 160);
            //c.Activate();

            var d = new NormalCardObject(World, new SimpleCardName(World, "『水符』热水"),
                new HotWaterTanmakuSequence(World, this, Times: 999999), new VapreonCardActivateAnimates(World),
                this, new LightBlueHalo(World, this), 100);

            a.SetEndCall(delegate
            {
                MoveToCenter();
                b.Activate();
            });
            b.SetEndCall(delegate
            {
                MoveToCenter();
                c.Activate();
            });
            c.SetEndCall(delegate
            {
                MoveToCenter();
                d.Activate();
            });
            d.SetEndCall(MoveAway);

            DispatcherTimer dpm = new DispatcherTimer();
            dpm.Interval = TimeSpan.FromMilliseconds(1500);
            dpm.Tick += delegate
            {
                a.Activate();
                dpm.Stop();
            };
            dpm.Start();

            return;
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
                        //player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                        obj.Dead = true;
                    }
                }
            }
        }
        DispatcherTimer timer;

        private void MoveToRandom()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(4000);
            var Interval = 500;
            timer.Tick += delegate
            {
                double x = rand.NextDouble(40, World.Width - 50 - this.Width);

                double duration = 800; // Math.Abs(x - X) * 8;

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
        void MoveToCenter()
        {
            if (timer.IsEnabled)
                timer.Stop();
            MoveTo((World.Width - Width) / 2, World.Height/5 - Height / 2, 400);
            timer.Start();
        }

    }
}
