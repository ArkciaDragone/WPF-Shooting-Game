using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class FireBallSkin:EmptyBulletSkin
    {
        public FireBallSkin()
        {
            string[] Fn = { "Sources/FireBall.png" };
            Ret = new Engine.ImageSprite(Fn, 27);
            Width = Height = 27;
        }
       
    }
}
