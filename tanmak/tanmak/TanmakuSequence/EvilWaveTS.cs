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
    public class EvilWaveTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        public EvilWaveTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin SkL=null,EmptyBulletSkin Skm=null,int Times = 999, EmptyBulletSkin Sks=null,
            int Ln = 70, int Mn = 70, int Sn = 40, 
            double Speed_Ave = 4, double Speed_Rg = 2,int Inverval = 40,int GCD = 20):
            base(world,parent)
        {
            this.GCD = GCD;
            if (SkL == null)
                SkL = new BlackBigTamaSkin();
            if (Skm == null)
                Skm = new BlackMediumTamaSkin();
            if (Sks == null)
                Sks = new BlackSmallTamaSkin();

            Bullets.Add(new RandomExposingBulleter(SkL,
                Ln, Speed_Ave, Speed_Rg, Parent, 111));
            Delay.Add(10);
            Interval.Add(Inverval);
            Bullets.Add(new RandomExposingBulleter(Skm,
                Mn, Speed_Ave, Speed_Rg, Parent, 222));
            Delay.Add(10);
            Interval.Add(Inverval);
            Bullets.Add(new RandomExposingBulleter(Sks,
                Sn, Speed_Ave, Speed_Rg, Parent, 123));
            Delay.Add(10);
            Interval.Add(Inverval);

            EndTick = Times * Inverval + 10 + GCD;

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
