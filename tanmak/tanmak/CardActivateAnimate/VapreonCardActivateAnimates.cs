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
    public class VapreonCardActivateAnimates : EmptyCardActivateAnimate
    {

        readonly int Len1 = 40;
        readonly int Len2 = 90;
        readonly int Len3 = 40;

        int MaxLen;
        int EndTick;
        double AlphaFunc(int Tick)
        {
            if (Tick <= Len1)
                return Tick * 1.0 / Len1;
            else if (Tick <= Len1 + Len2)
                return 1;
            else if (Tick <= Len1 + Len2 + Len3)
                return (Len1 + Len2 + Len3 - Tick) * 1.0 / Len3;
            else
                throw new Exception("???");
        }

        double XBegin, XEnd;
        double Ypos;
        double XFunc(int Tick)
        {
            if (Tick <= Len1)
                return XBegin + (Tick * 1.0 / Len1) * (XEnd - XBegin) * 0.4;
            else if (Tick <= Len1 + Len2)
                return XBegin + (XEnd - XBegin) * 0.4 + (Tick * 1.0 - Len1) / Len2 * (XEnd - XBegin) * 0.2;
            else if (Tick <= Len1 + Len2 + Len3)
                return XEnd - (Len1 + Len2 + Len3 - Tick * 1.0) / Len3 * (XEnd - XBegin) * 0.4 ;
            else
                throw new Exception("???");
        }
        int Tick = 0;

        public VapreonCardActivateAnimates(World world) : base(world)
        {
            MaxLen = Len1 + Len2 + Len3;
            string[] Fn = { "Sources/Illust/91677258_p0.png" };
            var Ht = world.Height * 1;
            Sprite = new Engine.ImageSprite(Fn, height:Ht);
            XBegin = world.Width / 6.0;
            XEnd = world.Width / (-4.0);
            Ypos = 0;// world.Height * 0.15;

            EndTick = MaxLen;

            X = XBegin;
            Y = Ypos;
        }

        
        public override void OnUpdate()
        {
            if(Tick >= EndTick)
            {
                DeathCall?.Invoke();
                Dead = true;
            }
            X = XFunc(Tick);
            ((ImageSprite)Sprite).SetAlpha(AlphaFunc(Tick));
            ++Tick;
        }

        public override void Activate()
        {
            Tick = 0;
            World.AddTopMost(this);
        }


    }
}
