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
    public class HotWaterTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        public HotWaterTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Ska = null,EmptyBulletSkin Skaa=null,EmptyBulletSkin Skb =null, int Times = 100,
            int LWaves=2, int WInter=4, int WOnter = 8, int WWays=35,
            double WASpeed = 0.12, double WRSpeed = 1.3,
            int DInter = 20, int lWaves=5, int lWays=10,double lSpeed = 0.9,int lInter = 9,
            int GCD = 45):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new SimpleLightBigTamaSkin();
            if (Skaa == null)
                Skaa = new SimpleDarkBlueBigTamaSkin();
            if (Skb == null)
                Skb = new IceBallSkin();

            int wi;
            for(wi=0;wi<LWaves;++wi)
            {
                Bullets.Add(new RotateBulleter(Ska, WWays, 0, WASpeed, 0, WRSpeed, parent));
                Delay.Add(wi * (WInter + WOnter));
                Interval.Add((LWaves * (WInter + WOnter)) + DInter * 2);

                Bullets.Add(new RotateBulleter(Skaa, WWays, 0, -WASpeed, 0, WRSpeed, parent));
                Delay.Add(wi * (WInter + WOnter) + WInter);
                Interval.Add((LWaves * (WInter + WOnter)) + DInter * 2);
            }

            for(wi=0;wi<lWaves;++wi)
            {
                Bullets.Add(new CircleBulleter(Skb, lWays, -10000, lSpeed, parent));
                Delay.Add((LWaves * (WInter + WOnter)) + DInter + wi * lInter);
                Interval.Add((LWaves * (WInter + WOnter)) + DInter * 2);
            }
            
            EndTick = Times * ((LWaves * (WInter + WOnter)) + DInter * 2) + GCD;

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
