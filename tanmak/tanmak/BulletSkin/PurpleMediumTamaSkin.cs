using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class PurpleMediumTamaSkin : EmptyBulletSkin
    {
        public PurpleMediumTamaSkin()
        {
            string[] Fn = { "Sources/MediumTama_Purple.png" };
            Ret = new Engine.ImageSprite(Fn, 27, 0);
            Width = Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0);
        }
       
    }
}
