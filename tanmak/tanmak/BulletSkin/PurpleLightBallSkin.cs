using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class PurpleLightBallSkin : EmptyBulletSkin
    {
        public PurpleLightBallSkin()
        {
            string[] Fn = { "Sources/Purple_Light_Ball.png" };
            Ret = new Engine.ImageSprite(Fn, 19, 19);
            Width = Height = ((Engine.ImageSprite)Ret).GetHeight();
            ((Engine.ImageSprite)Ret).SetAngularSpeed(0);
        }
       
    }
}
