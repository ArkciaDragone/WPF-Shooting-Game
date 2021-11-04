using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tanmak.BulletSkin
{
    public class IceShardSkin:EmptyBulletSkin
    {
        public IceShardSkin()
        {
            string[] Fn = { "Sources/IceShard.png" };
            Ret = new Engine.ImageSprite(Fn, 9, 0);
            Width = ((Engine.ImageSprite)Ret).GetWidth();
            Height = ((Engine.ImageSprite)Ret).GetHeight();
        }
       
    }
}
