using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class LightBigTamaSkin:EmptyBulletSkin
    {
        public LightBigTamaSkin()
        {
            string[] Fn = { "Sources/BigTama_Light.png" };
            Ret = new Engine.ImageSprite(Fn, 62, 0);
            Width = Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
