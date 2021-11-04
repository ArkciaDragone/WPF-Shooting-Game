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
    public class EmptyHalo:GameObject
    {
        GameObject Boss;
        public EmptyHalo(World world, GameObject Boss):base(world)
        {
            this.Boss = Boss;
        }

        public int Size = 100;
        public double Rot = 5;

        public override void OnUpdate()
        {
            X = Boss.X + Boss.Width / 2 - Size / 2;
            Y = Boss.Y + Boss.Height / 2 - Size / 2;
        }
        public void Activate()
        {
            World.AddUnder(this);
        }
        public void Stop()
        {
            Dead = true;
        }
    }
}
