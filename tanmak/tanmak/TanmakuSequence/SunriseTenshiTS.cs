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
    public class SunriseTenshiTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        public SunriseTenshiTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Ska = null, EmptyBulletSkin Skb = null, int Times = 4,
            int Ways = 30, int Waves = 4, int iInter = 4, int Inter = 6,
            int GlobalWait = 65, double av=0.15, double rv=1.2, int GCD = 20):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new SimpleYellowBigTamaSkin();
            if (Skb == null)
                Skb = new SimpleRedBigTamaSkin();

            for (int wi = 0; wi < Waves; ++wi)
            {
                Bullets.Add(new RotateBulleter(Ska, Ways, 0, av, 0, rv, Parent));
                Delay.Add(wi *(Inter + iInter));
                Interval.Add(GlobalWait);

                Bullets.Add(new RotateBulleter(Skb, Ways, 0, -av, 0, rv, Parent));
                Delay.Add(wi*(Inter+iInter) + iInter);
                Interval.Add(GlobalWait);
            }
            EndTick = Times * GlobalWait + GCD;

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
