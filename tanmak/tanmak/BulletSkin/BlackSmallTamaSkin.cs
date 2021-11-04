using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class BlackSmallTamaSkin:EmptyBulletSkin
    {
        public BlackSmallTamaSkin()
        {
            string[] Fn = { "Sources/SmallTama_Black.png" };
            Ret = new Engine.ImageSprite(Fn, 13, 0);
            Width = Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
