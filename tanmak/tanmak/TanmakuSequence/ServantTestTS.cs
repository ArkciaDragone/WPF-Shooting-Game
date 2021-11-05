using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Windows.Threading;
using tanmak.BulletSkin;
using tanmak.Game;
using tanmak.Servant;
using System.Windows;

namespace tanmak.TanmakuSequence
{
    public class ServantTestTenmakuSequence: EmptyTanmakuSequence
    {
        EmptyTanmakuSequence Ea, Eb;
        bool Cycle;
        NormalServant Sa, Sb;
        public ServantTestTenmakuSequence(World world, GameObject parent, GameObject player,int EndTick = 99999):
            base(world,parent)
        {
            Point PFunc(int Tick)
            {
                Point ret = new Point();
                double r = 240;
                double av = Tick / 90.0;
                ret.X = r * Math.Cos(av) + World.Width / 2;
                ret.Y = r * Math.Sin(av) + World.Height / 2;
                return ret;
            };

            Point PFuncb(int Tick)
            {
                Point ret = new Point();
                double r = 240;
                double av = -Tick / 90.0;
                ret.X = r * Math.Cos(av) + World.Width / 2;
                ret.Y = r * Math.Sin(av) + World.Height / 2;
                return ret;
            };

            var c = new NormalServant(World, player, parent, PFunc);
            var ca = new NormalServant(World, player, parent, PFuncb);

            var a = new LinkingTenmakuSequence(World, c,
                new SunriseTenshiTanmakuSequence(World, c, Times: 1, Waves: 1, Ways: 10, GCD: 5, GlobalWait: 10),
                new HotWaterTanmakuSequence(World, c, Times: 1, LWaves: 1, WWays: 10, GCD: 5),
                true);
            var aa = new LinkingTenmakuSequence(World, ca,
                new SunriseTenshiTanmakuSequence(World, ca, Times: 1, Waves: 1, Ways: 10, GCD: 5, GlobalWait: 10),
                new HotWaterTanmakuSequence(World, ca, Times: 1, LWaves: 1, WWays: 10, GCD: 5),
                true);
            ca.SetSequence(aa);
            c.SetSequence(a);
            Sa = c;
            Sb = ca;

            Ea = a;
            Eb = aa;
            this.EndTick = EndTick;
        }

        public override void Activate()
        {
            Ea.SetEndCall(DeathCall);
            Sa.Activate();
            Sb.Activate();
            
        }
        public override void Stop()
        {
            Sa.Stop();
            Sb.Stop();
        }

    }
}
