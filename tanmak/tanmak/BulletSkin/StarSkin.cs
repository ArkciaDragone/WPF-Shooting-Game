using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class StarSkin:EmptyBulletSkin
    {
        public StarSkin()
        {
            string[] Fn = { "Sources/Star.png" };
            Ret = new Engine.ImageSprite(Fn, 29, 29);
            Width = ((Engine.ImageSprite)Ret).GetWidth();
            Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0.5);
        }
       
    }
}
