using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;

namespace tanmak.BulletSkin
{
    public class EmptyBulletSkin
    {
        public int Width, Height;
        protected Sprite Ret;
        public EmptyBulletSkin()
        {
            Ret = new Engine.CircleSprite(new SolidColorBrush(Color.FromRgb(0, 255, 255)), 5);
            Width = Height = 10;
        }

        public Sprite GetSprit()
        {
            return Ret;
        }
    }
}
