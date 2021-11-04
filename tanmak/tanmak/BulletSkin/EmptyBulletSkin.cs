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
            
        }

        public virtual Sprite GetSprit()
        {
            return Ret;
        }
    }
}
