using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.BulletSkin
{
    public class SimpleIceBallSkin:EmptyBulletSkin
    {
        public SimpleIceBallSkin(int r=15)
        {
            Ret = new tanmak.Engine.CircleSprite(
                new SolidColorBrush(Color.FromArgb(200, 100, 100, 255)), r);
            Width = Height = r * 2;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
