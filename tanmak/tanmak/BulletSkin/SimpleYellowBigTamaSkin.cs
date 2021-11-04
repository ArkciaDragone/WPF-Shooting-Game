using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.BulletSkin
{
    public class SimpleYellowBigTamaSkin:EmptyBulletSkin
    {
        public SimpleYellowBigTamaSkin()
        {
            Ret = new tanmak.Engine.Circle2Sprite(
                new SolidColorBrush(Color.FromArgb(230, 220, 220, 50)), 15,
                new SolidColorBrush(Color.FromArgb(150, 200, 200, 50)), 30);
            Width = Height = 30;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
