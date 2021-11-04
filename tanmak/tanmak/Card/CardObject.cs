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
using tanmak.Halo;

namespace tanmak.Card
{
    public class CardObject
    {
        public delegate void CALL_BACK();
        public CALL_BACK DeathCall;
        public EmptyTanmakuSequence Tanmaku;
        public SimpleCardName CardName;
        public EmptyCardActivateAnimate CardAnimate;
        public World world;
        public GameObject Boss;
        public EmptyHalo Halo;
        public CardObject(World world, SimpleCardName Name, EmptyTanmakuSequence Tanmk,
            EmptyCardActivateAnimate CardAnimate, GameObject Boss, EmptyHalo Halo)
        {
            this.world = world;
            this.CardName = Name;
            this.Tanmaku = Tanmk;
            this.CardAnimate = CardAnimate;
            this.Boss = Boss;
            this.Halo = Halo;
        }
        public void SetEndCall(CALL_BACK c) => DeathCall = c;
        public virtual void Activate()
        {

        }


    }
}
