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
    public class ExposionAroundTS: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        
        int Tick;
        Engine.Random rand = new Engine.Random();
        int GCD;
        public ExposionAroundTS(World world, GameObject parent,
            EmptyBulletSkin Ska = null, EmptyBulletSkin Skb = null,
            int Bombs=6, int split_num=40, double R=100, int WalkTime = 60,
            int ArriveTime = 60, int Throw_Inverval=2, double split_v=3,
            double split_v_range = 2, double sAngle = -9999.9, int GCD=20):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new SimpleRedBigTamaSkin();
            if (Skb == null)
                Skb = new SimpleIceBallSkin(11);
            double Ang = sAngle;
            if (Ang < -9999)
                Ang = rand.NextDouble(0, 360);
            for (int wi = 0; wi < Bombs; ++wi)
            {
                Ang += (360.0/Bombs);
                Ang %= 360;
                double tx = R * Math.Cos(Ang * Math.PI / 180);
                double ty = R * Math.Sin(Ang * Math.PI / 180);
                Bullets.Add(new ExposionBulleter(Ska,Skb,
                    (int)tx,(int)ty,WalkTime,ArriveTime,
                    split_num,split_v,split_v_range,Parent));
                Delay.Add(wi * Throw_Inverval);
                Interval.Add(998244353);
            }
            EndTick = (Bombs - 1) * Throw_Inverval + 1 + GCD;

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
