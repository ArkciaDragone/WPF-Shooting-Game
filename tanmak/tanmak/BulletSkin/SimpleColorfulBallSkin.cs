using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using tanmak.Engine;

namespace tanmak.BulletSkin
{
    public class SimpleColorfulBallSkin:EmptyBulletSkin
    {
        static readonly byte GLOBAL_ALPHA = 150;
        static System.Random Rd = new System.Random();
        static Color[] Clrs = new Color[]{ 
            Color.FromArgb(GLOBAL_ALPHA,255,0,0),
            Color.FromArgb(GLOBAL_ALPHA,255,165,0),
            Color.FromArgb(GLOBAL_ALPHA,255,255,0),
            Color.FromArgb(GLOBAL_ALPHA,0,255,0),
            Color.FromArgb(GLOBAL_ALPHA,0,255,255),
            Color.FromArgb(GLOBAL_ALPHA,0,0,255),
            Color.FromArgb(GLOBAL_ALPHA,139,0,255)
        };
        static Sprite[] Rets;
        public SimpleColorfulBallSkin(int R = 15)
        {
            if(Rets == null)
            {
                Rets = new Sprite[Clrs.Length];
                int wi;
                for (wi = 0; wi < Clrs.Length; ++wi)
                    Rets[wi] = new Engine.CircleSprite(new SolidColorBrush(Clrs[wi]), R);
            }
            Width = Height = R*2;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
        public override Sprite GetSprit()
        {
            return Rets[Rd.Next(Rets.Length)];
        }


    }
}
