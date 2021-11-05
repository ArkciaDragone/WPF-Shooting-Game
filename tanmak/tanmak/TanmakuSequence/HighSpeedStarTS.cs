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
    public class HighSpeedStarTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        public HighSpeedStarTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Ska = null, int Times = 100,
            int Ways = 3, int Inter = 3,
            double speed_mean = 5, double speed_adj = 2,
            double Rad_range = 30, int GCD = 20):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new StarSkin();

            Bullets.Add(new RandomFallBulleter(Ska, speed_mean, speed_adj,
                Ways, Rad_range, Parent));
            Delay.Add(0);
            Interval.Add(Inter);
            
            EndTick = Times * Inter + GCD;

            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(50);
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
            Tick = 0;
            dispatcher.Start();
        }

    }
}
