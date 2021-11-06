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
    public class Eevees : GameObject
    {
        Engine.Random rand = new Engine.Random();
        List<Bulleter> bullets = new List<Bulleter>();
        List<int> Inter = new List<int>();
        List<int> Delay = new List<int>();
        DispatcherTimer dispancer;
        ObjPlayer player;

        public Eevees(World world, ObjPlayer player) : base(world)
        {
            MoveAway();
            this.player = player;
            string[] efn = new string[13];
            for (int wi = 0; wi < 13; ++wi)
                efn[wi] = "Sources/Ens/" + (wi + 1).ToString() + ".png";
            Sprite = new ImageSprite(efn, 60, animationWait: 1);

            Width = ((ImageSprite)Sprite).GetWidth();
            Height = ((ImageSprite)Sprite).GetHeight();
            MoveToRandom();
            //Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            var a = new NormalCardObject(World, new SimpleCardName(World, "万变『高速星星』"),
                new HighSpeedStarTanmakuSequence(World, this, Times: 999999), new EeveeCardActivateAnimate(World),
                this, new OrangeHallo(World, this), 110);
            //a.Activate();

            var b = new NormalCardObject(World, new SimpleCardName(World, "冻风『冰冻之风』"),
                new FreezingWindTanmakuSequence(World, this, Times: 999999), new GlaceonCardActivateAnimate(World),
                this, new BlueHalo(World, this), 90);
            //b.Activate();

            var c = new NormalCardObject(World, new SimpleCardName(World, "恶符『恶之波动』"),
                new EvilWaveTanmakuSequence(World, this, Times: 999999), new UmbreonCardActivateAnimates(World),
                this, new WhiteHalo(World, this), 180);
            //c.Activate();

            var d = new TimingCardObject(World, new SimpleCardName(World, "『光墙』"),
                new LightWallTanmakuSequence(World, this, player), player,
                new SylveonCardActivateAnimates(world), this, new RedHalo(World, this), 20);

            var e = new NormalCardObject(World, new SimpleCardName(World, "水符『冲浪』"),
                new HotWaterTanmakuSequence(World, this, Times: 999999), new VapreonCardActivateAnimates(World),
                this, new LightBlueHalo(World, this), 140);


            a.SetEndCall(delegate
            {
                b.Activate();
            });
            b.SetEndCall(delegate
            {
                c.Activate();
            });
            c.SetEndCall(delegate
            {
                d.Activate();
            });
            d.SetEndCall(e.Activate);
            e.SetEndCall(delegate
                {
                MoveAway();
                player.ScoreManager.Win = true;
            });

            DispatcherTimer dpm = new DispatcherTimer();
            dpm.Interval = TimeSpan.FromMilliseconds(1000);
            dpm.Tick += delegate
            {
                a.Activate();
                dpm.Stop();
            };
            dpm.Start();

            return;
        }
        bool IsHited(GameObject obj)
        {
            return obj.X >= X && obj.X <= X + Width &&
                obj.Y >= Y && obj.Y <= Y + Height;
        }
        public override void OnUpdate()
        {
            foreach (GameObject obj in World.Objects)
            {
                if (!obj.Dead && obj is ObjOwnBullet)
                {
                    if (this.IsHited(obj))
                    {
                        HitCount++;
                        //player.ScoreManager.EnemyHiited(ScoreManager.NormalMissileDamage);
                        obj.Dead = true;
                    }
                }
            }
        }
        private void MoveToRandom()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
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
            if (timer != null &&timer.IsEnabled)
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
