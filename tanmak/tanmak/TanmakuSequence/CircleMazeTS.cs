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
    public class CircleMazeTanmakuSequence: EmptyTanmakuSequence
    {
        double LastAng = 0;
        List<CircleCenteringBulleters> Bullets=new List<CircleCenteringBulleters>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        static Engine.Random Rand= new Engine.Random();
        public CircleMazeTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Ska = null, int Times = 4,
            int Waves = 3, int Ways = 40, int TimeDiv = 110, int WaveDiv = 5,
            double dAngle = 0.6, double RStart = 60, double RDiv = 55,double InitR=400,
            int Ticks1 = 180, int Ticks3 = 70, int Ticks2_s =300,
            double Ticks2_down = 0.8, int GCD=0):
            base(world,parent)
        {
            this.GCD = GCD;
            if (Ska == null)
                Ska = new PurpleMediumTamaSkin();


            int wi;
            for(wi=1;wi<=Waves;++wi)
            {
                Bullets.Add(new CircleCenteringBulleters(
                    Ska, Ways, InitR, RStart + RDiv * (wi - 1), Ticks1, Ticks3, dAngle - 0.1*(wi-1), Parent));
                Delay.Add((wi - 1) * WaveDiv);
                Interval.Add(TimeDiv + (WaveDiv * Waves));
            }
            EndTick = (TimeDiv + (WaveDiv * Waves)) * Times + GCD;
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
                   int Tims = Tick / (TimeDiv + (WaveDiv * Waves));
                    for (int bulletIndex = 0; bulletIndex < Bullets.Count; ++bulletIndex)
                        if (Delay[bulletIndex] <= Tick)
                            if ((Tick - Delay[bulletIndex]) % Interval[bulletIndex] == 0)
                            {
                                LastAng = LastAng + Math.PI / 5 + Rand.NextDouble(0, Math.PI);
                                LastAng %= 360;
                                Bullets[bulletIndex].Shoot(LastAng,
                                    Tick2: (int)(Ticks2_s * Math.Pow(Ticks2_down, Tims)));
                            }
                    
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
