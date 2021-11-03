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
            MoveTo(World.Width / 2 - Width / 2, World.Height / 3 - Height / 2, 20);

            Sprite = new RenctangleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 0)), Width, Height);

            this.player = player;

            double av = 0.7, rv = 1.45;
            var I = new IceBallSkin();
            var F = new FireBallSkin();
            bullets.Add(new RotateBulleter(I, 20, 0, av, 0, rv, this));
            Inter.Add(2);
            Delay.Add(0);
            bullets.Add(new RotateBulleter(F, 20, 0, -av, 0, rv, this));
            Inter.Add(2);
            Delay.Add(1);
            dispancer = new DispatcherTimer();
            int Tick = 0;
            dispancer.Interval = TimeSpan.FromMilliseconds(600);
            dispancer.Tick += delegate
            {
                ++Tick;
               for(int bulletIndex=0; bulletIndex<bullets.Count;++bulletIndex)
                    if(Delay[bulletIndex] < Tick)
                    if((Tick - Delay[bulletIndex]) % Inter[bulletIndex] == 0)
                        bullets[bulletIndex].Shoot();

                dispancer.Interval = TimeSpan.FromMilliseconds(350);
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
                    }
                }
            }
        }

    }
}
