using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.TanmakuSequence;
using tanmak.CardActivateAnimate;
using tanmak.CardNameShow;
using tanmak.ProcessingBar;
using tanmak.Game;
using tanmak.Engine;
using System.Windows.Threading;
using System.Threading;

namespace tanmak.Card
{
    public class TimingCardObject:CardObject
    {
        SimpleTimingBar Bar;
        CALL_BACK Start;
        public TimingCardObject(World world, SimpleCardName Name, EmptyTanmakuSequence Tanmk,
            EmptyCardActivateAnimate CardAnimate, GameObject Boss, int GCD=40):base(world,Name,Tanmk,CardAnimate,Boss)
        {
            Bar = new SimpleTimingBar(world, Tanmaku.EndTick);

            Start = delegate
            {
                this.CardAnimate.Activate();
                this.CardName.Activate();
                
            };
            this.CardAnimate.SetEndCall(delegate { 
                Tanmaku.Activate(); 
                this.Bar.Activate(); });
            this.Bar.SetEndCall(delegate
            {
                Tanmaku.Stop();
                this.CardName.Stop();
                var a = new DispatcherTimer();
                a.Interval = TimeSpan.FromMilliseconds(GCD * 50);
                a.Tick += delegate
                {
                    if(a.IsEnabled)
                        DeathCall?.Invoke();
                    a.Stop();
                };
                a.Start();
            });
        }
        public override void Activate()
        {
            Start();
        }
    }
}
