using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace tanmak.CardNameShow
{
    public class SimpleCardName:GameObject
    {
        DispatcherTimer dispatcher;
        readonly int Len1 = 140, Len2 = 30;

        string Text;

        double XPos = 10;
        double YBegin;
        double YEnd = 20;
        int Ticks = 0;
        double YFunc(int Tick)
        {
            if (Tick <= Len1)
                return YBegin - Tick / 10.0;
            else if (Tick <= Len1 + Len2)
                return YBegin - Len1 / 10.0 - (YBegin - Len1 / 10.0 - YEnd) * (Tick - Len1) / Len2;
            else
                return YEnd;
        }
        public SimpleCardName(World world, string Name):base(world)
        {
            Text = Name;
            YBegin = world.Height / 3 * 2;
        }
        public void Activate()
        {
            World.AddTopTopMost(this);
        }
        public override void OnUpdate()
        {
            ++Ticks;
            Y = YFunc(Ticks);
        }
        public override void OnRender(DrawingContext dc)
        {
            dc.DrawText(new FormattedText(Text, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, 18, Brushes.Black), 
                new System.Windows.Point(XPos,Y));
        }
        public void Stop()
        {
            Dead = true;
        }
    }
}
