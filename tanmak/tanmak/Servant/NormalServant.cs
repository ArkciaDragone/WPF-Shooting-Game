using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Windows.Threading;
using tanmak.BulletSkin;
using tanmak.Game;
using tanmak.TanmakuSequence;
using System.Windows;
using System.Windows.Media;

namespace tanmak.Servant
{
    class NormalServant : GameObject
    {
        public delegate Point POS_FUNC(int Tick);
        public delegate void CALL_BACK();

        CALL_BACK DeathCall;
        POS_FUNC PosFunc;
        GameObject Player, Boss;
        EmptyTanmakuSequence Seq;

        public NormalServant(World world, GameObject Player, GameObject Boss,
            POS_FUNC Pf, Sprite Sprite=null):base(world)
        {
            if(Sprite == null)
            {
                string[] Fn = { "Sources/Halos/WhiteHalo.png" };
                Sprite = new Engine.ImageSprite(Fn, 50);
                ((ImageSprite)Sprite).SetAngularSpeed(10);
            }
            this.Sprite = Sprite;
            Width = ((ImageSprite)Sprite).GetWidth();
            Height = ((ImageSprite)Sprite).GetHeight();
            PosFunc = Pf;
            this.Player = Player;
            this.Boss = Boss;
        }

        public void SetSequence(EmptyTanmakuSequence Ets) => Seq = Ets;

        public void SetEndCall(CALL_BACK c) => DeathCall = c;

        public void Activate()
        {
            Seq.DeathCall += Stop;
            World.AddUnder(this);
            Seq.Activate();
        }

        public void Stop()
        {
            if(!Dead)
                DeathCall?.Invoke();
            Dead = true;
            Seq.Stop();
        }
        int Tick;
        public override void OnUpdate()
        {
            Point Ps = PosFunc(Tick);
            X = Ps.X - Width/2;
            Y = Ps.Y - Height/2;
            ++Tick;
        }
    }
}
