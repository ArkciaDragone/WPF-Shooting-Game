using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Windows.Threading;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows;

namespace tanmak.TanmakuSequence
{
    public class LightWallTanmakuSequence: EmptyTanmakuSequence
    {
        List<Bulleter> Bullets=new List<Bulleter>();
        List<int> Delay=new List<int>(), Interval=new List<int>();
        int GCD;
        int Tick;
        delegate void START();
        START Start;

        EmptyTanmakuSequence S1, S2;
        public LightWallTanmakuSequence(World world, GameObject parent,GameObject player
           ):
            base(world,parent)
        {

            var Zero =new LinkingTenmakuSequence(World, parent,
                new DelayTanmakuSequence(World, parent,80),
                new CircleMazeTanmakuSequence(world, parent, Times: 1, Waves: 1, Ways: 40, Ticks2_s: 140));
            var One = new LinkingTenmakuSequence(World, parent,
                Zero,
                new CircleMazeTanmakuSequence(World, parent, Times: 3, Waves: 2, Ticks2_s: 200));
            var Two = new LinkingTenmakuSequence(World, parent,
                One,
                new CircleMazeTanmakuSequence(World, parent, Times: 4, Waves: 3));
            var Three = new LinkingTenmakuSequence(World, parent,
                Two,
                new CircleMazeTanmakuSequence(World, parent, Times: 1, Waves: 4, RDiv: 40, TimeDiv: 130));
            var Limit = new AreaLimitingServantTanmakuSequence(World, parent, player,Mid: delegate
            {
                Three.Activate();
            });
            
            
            EndTick = Three.EndTick;
            S1 = Limit;
            S2 = Three;
            Start = delegate
            {
                Limit.Activate();
            };

            Three.SetEndCall(delegate
            {
                S1.Stop();
                DeathCall?.Invoke();
            });
        }

        public override void Activate()
        {
            Start();
        }

        public override Point GetMovingTo()
        {
            return new Point(World.Width / 2 - Parent.Width / 2, World.Height / 2 - Parent.Width / 2);
        }

        public override void Stop()
        {
            S1.Stop();
            S2.Stop();
        }

    }
}
