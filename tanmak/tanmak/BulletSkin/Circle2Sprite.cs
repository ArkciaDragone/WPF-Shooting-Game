using System.Windows;
using System.Windows.Media;
using System;
using tanmak.Engine;
namespace tanmak.Engine
{
    public class Circle2Sprite : Sprite
    {
        double in_radius = 5;
        double ou_radius = 10;
        Brush brush_i,brush_o;


        public Circle2Sprite(Brush brush_i, double radius_i, Brush brush_o, double radius_o)
        {
            this.brush_i = brush_i;
            this.brush_o = brush_o;

            this.in_radius = radius_i;
            this.ou_radius = radius_o;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawEllipse(brush_i, null, new Point(Parent.X + in_radius, Parent.Y + in_radius), in_radius, in_radius);
            dc.DrawEllipse(brush_o, null, new Point(Parent.X + in_radius, Parent.Y + in_radius), ou_radius, ou_radius);
        }
    }
}
