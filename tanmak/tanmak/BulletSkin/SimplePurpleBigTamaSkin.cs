using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.BulletSkin
{
    public class SimplePurpleBigTamaSkin : EmptyBulletSkin
    {
        public SimplePurpleBigTamaSkin()
        {
            Ret = new tanmak.Engine.Circle2Sprite(
                new SolidColorBrush(Color.FromArgb(230, 138, 43, 226)), 15,
                new SolidColorBrush(Color.FromArgb(110, 138, 43, 226)), 30);
            Width = Height = 30;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
