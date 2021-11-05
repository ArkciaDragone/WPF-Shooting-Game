using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Media;

namespace tanmak.Halo
{
    public class OrangeHallo : EmptyHalo
    {
        GameObject Boss;
        public OrangeHallo(World world, GameObject Boss):base(world, Boss)
        {
            string[] Fn = { "Sources/Halos/OrangeHalo.png" };
            Sprite = new Engine.ImageSprite(Fn, Size, Size);
            ((ImageSprite)Sprite).SetAngularSpeed(Rot);
        }

    }
}
