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
using System.Threading;
using System.Windows.Threading;
using tanmak.Halo;

namespace tanmak.Card
{
    public class NormalCardObject:CardObject
    {
        SimpleHealthBar Bar;
        CALL_BACK Start;
        public NormalCardObject(World world, SimpleCardName Name, EmptyTanmakuSequence Tanmk,
            EmptyCardActivateAnimate CardAnimate, GameObject Boss,EmptyHalo Halo, int MaxHP, int GCD = 40):base(world,Name,Tanmk,CardAnimate,Boss, Halo)
        {
            Bar = new SimpleHealthBar(world, MaxHP, Boss);

            Start = delegate
            {
                this.CardAnimate.Activate();
                this.CardName.Activate();
                this.Halo.Activate();
            };
            this.CardAnimate.SetEndCall(delegate { 
                Tanmaku.Activate(); 
                this.Bar.Activate(); });
            this.Bar.SetEndCall(delegate
            {
                Tanmaku.Stop();
                this.CardName.Stop();
                this.Halo.Stop();
                var a = new DispatcherTimer();
                a.Interval = TimeSpan.FromMilliseconds(GCD * 50);
                int LTick = 0;
                a.Tick += delegate
                {
                    
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
