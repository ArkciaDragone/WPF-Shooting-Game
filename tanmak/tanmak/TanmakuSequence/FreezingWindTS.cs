using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Windows.Threading;
using tanmak.BulletSkin;
using tanmak.Game;

namespace tanmak.TanmakuSequence
{
    public class FreezingWindTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        public FreezingWindTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Ska = null,EmptyBulletSkin Skb=null, int Times = 1000,
            int WallInter = 20, int StormInter = 7, double WallSpeed = 1.2,
            double WallRange = 0.8,double SplitSize = 0.12,
            double speed_mean = 3, double speed_adj = 1,
            double Rad_range = 10, int GCD = 45):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new SimpleBlueBigTamaSkin();
            if (Skb == null)
                Skb = new IceShardSkin();

            Bullets.Add(new RandomFallBulleter(Skb, speed_mean, speed_adj,
                3, Rad_range, Parent));
            Delay.Add(10);
            Interval.Add(StormInter);
            Bullets.Add(new RandomWallBulleter(Ska, WallSpeed, WallRange,
                SplitSize, Parent));
            Delay.Add(0);
            Interval.Add(WallInter);
            
            EndTick = Times * WallInter + GCD;

            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(50);
            Tick = 0;
            dispatcher.Tick += delegate
            {

                if (Tick >= EndTick - GCD) 
                {
                    if (Tick >= EndTick)
                    {
                        DeathCall?.Invoke();
                        dispatcher.Stop();
                    }
                }
                else
                {
                    for (int bulletIndex = 0; bulletIndex < Bullets.Count; ++bulletIndex)
                        if (Delay[bulletIndex] <= Tick)
                            if ((Tick - Delay[bulletIndex]) % Interval[bulletIndex] == 0)
                                Bullets[bulletIndex].Shoot();
                    
                }
                Tick++;

            };
        }

        public override void Activate()
        {
            dispatcher.Start();
        }

    }
}
