using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Threading;
using System.Windows;


namespace tanmak.TanmakuSequence
{
    public class EmptyTanmakuSequence
    {
        public World World;
        public GameObject Parent;
        public CALL_BACK DeathCall;
        public int EndTick;
        public DispatcherTimer dispatcher;

        static Engine.Random rand = new Engine.Random();
        public EmptyTanmakuSequence(World world, GameObject parent)
        {
            Parent = parent;
            World = world;
        }
        public void SetEndCall(CALL_BACK c) => DeathCall = c;

        public delegate void CALL_BACK();
        public virtual void Activate()
        {

        }
        public virtual void Stop()
        {
            dispatcher.Stop();
        }
        public virtual Point GetMovingTo()
        {
            return new Point(World.Width / 2 - Parent.Width / 2, World.Height / 5 - Parent.Height / 2);
        }
    }
}