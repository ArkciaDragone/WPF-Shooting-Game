using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class BlackMediumTamaSkin:EmptyBulletSkin
    {
        public BlackMediumTamaSkin()
        {
            string[] Fn = { "Sources/MediumTama_Black.png" };
            Ret = new Engine.ImageSprite(Fn, 26, 0);
            Width = Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
