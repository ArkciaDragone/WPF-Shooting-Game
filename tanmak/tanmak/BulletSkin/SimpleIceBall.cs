﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace tanmak.BulletSkin
{
    public class SimpleIceBall:EmptyBulletSkin
    {
        public SimpleIceBall()
        {
            Ret = new tanmak.Engine.CircleSprite(
                new SolidColorBrush(Color.FromArgb(200, 100, 100, 255)), 15);
            Width = Height = 30;//((Engine.ImageSprite)Ret).GetHeight();
            //((Engine.ImageSprite)Ret).SetAngularSpeed(0.6);
        }
       
    }
}
