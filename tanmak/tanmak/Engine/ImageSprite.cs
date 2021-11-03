using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace tanmak.Engine
{
    public class ImageSprite : Sprite
    {
        double _width = 5;
        ImageSource source;

        public ImageSprite(string filename, double width)
        {
            source = new BitmapImage(new Uri(filename));
            _width = width;
        }

        public ImageSprite(Uri uri) => source = new BitmapImage(uri);
        public ImageSprite(BitmapImage img) => source = img;

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            dc.DrawImage(source, new Rect(new Point(Parent.X, Parent.Y), new Size(_width, source.Height * _width / source.Width)));
        }
    }
}
