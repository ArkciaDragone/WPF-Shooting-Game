using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.BulletSkin
{
    public class SimpleRedBigTamaSkin:EmptyBulletSkin
    {
        public SimpleRedBigTamaSkin()
        {
            Ret = new tanmak.Engine.Circle2Sprite(
                new SolidColorBrush(Color.FromArgb(230, 230, 0, 0)), 15,
                new SolidColorBrush(Color.FromArgb(120, 230, 120, 120)), 30);
            Width = Height = 30;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
