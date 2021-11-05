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
    public class LinkingTenmakuSequence: EmptyTanmakuSequence
    {
        EmptyTanmakuSequence Ea, Eb;
        EmptyTanmakuSequence ENow;
        bool Cycle;
        public LinkingTenmakuSequence(World world, GameObject parent,
            EmptyTanmakuSequence Ea, EmptyTanmakuSequence Eb,
            bool Cycle = false):
            base(world,parent)
        {

            ENow = null;
            this.Ea = Ea;
            this.Eb = Eb;
            this.Cycle = Cycle;
            Ea.SetEndCall(delegate
            {
                ENow = this.Eb;
                Eb.Activate();
            });
            EndTick = Ea.EndTick + Eb.EndTick;
            if (Cycle)
                EndTick = 999999999;
        }

        public override void Activate()
        {
            if (!Cycle)
                Eb.SetEndCall(DeathCall);
            else
                Eb.SetEndCall(Ea.Activate);
            ENow = Ea;
            Ea.Activate();
        }
        public override void Stop()
        {
            ENow.Stop();
        }

    }
}
