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
using tanmak.Halo;
using System.Windows;

namespace tanmak.Card
{
    public class TimingCardObject:CardObject
    {
        SimpleTimingBar Bar;
        CALL_BACK Start;
        public TimingCardObject(World world, SimpleCardName Name, EmptyTanmakuSequence Tanmk,ObjPlayer Player,
            EmptyCardActivateAnimate CardAnimate, GameObject Boss, EmptyHalo Halo, int GCD=40):base(world,Name,Tanmk,CardAnimate,Boss, Halo)
        {
            Bar = new SimpleTimingBar(world, Tanmaku.EndTick, Player);

            Start = delegate
            {
                this.CardAnimate.Activate();
                this.CardName.Activate();
                this.Halo.Activate();
                Point p = this.Tanmaku.GetMovingTo();
                this.Boss.timer.Stop();
                this.Boss.MoveTo(p.X, p.Y, MovingTime);
            };
            this.CardAnimate.SetEndCall(delegate { 
                Tanmaku.Activate(); 
                this.Bar.Activate();
                this.Boss.MoveAway();
            });
            this.Bar.SetEndCall(delegate
            {
                this.Halo.Stop();
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
                Boss.MoveTo(world.Width / 2 - Boss.Width / 2, world.Height / 5 - Boss.Height / 5, 700);
                Boss.timer.Stop();
            });
        }
        public override void Activate()
        {
            Start();
        }
    }
}
