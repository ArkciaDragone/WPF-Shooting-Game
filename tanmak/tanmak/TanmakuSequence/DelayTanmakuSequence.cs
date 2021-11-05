using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Threading;

namespace tanmak.TanmakuSequence
{
    public class DelayTanmakuSequence:EmptyTanmakuSequence
    {

        public DelayTanmakuSequence(World world, GameObject parent,int ticks):base(world,parent)
        {
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(ticks * 50);
            EndTick = ticks;
            dispatcher.Start();
        }

        public override void Activate()
        {
            dispatcher.Tick += delegate
            {
                DeathCall?.Invoke();
                dispatcher.Stop();
            };
        }

    }
}
