using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class IceBallSkin:EmptyBulletSkin
    {
        public IceBallSkin()
        {
            string[] Fn = { "Sources/IceBall.png" };
            Ret = new Engine.ImageSprite(Fn, 27);
            Width = Height = 27;
        }
       
    }
}
