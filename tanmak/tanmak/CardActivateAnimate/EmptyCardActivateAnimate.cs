using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Media;

namespace tanmak.CardActivateAnimate
{
    public class EmptyCardActivateAnimate:GameObject
    {
        public delegate void CALL_BACK();
        public CALL_BACK DeathCall;
        public EmptyCardActivateAnimate(World World):base(World)
        {

        }

        override public void OnUpdate()
        {
            base.OnUpdate();
        }
        override public void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
        virtual public void Activate()
        {


        }
        public void SetEndCall(CALL_BACK c) => DeathCall = c;
    }
}
