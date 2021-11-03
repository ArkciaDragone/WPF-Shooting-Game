using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace tanmak.Engine
{
    public class ImageSprite : Sprite
    {
        double _width, _height;
        ImageSource[] sources;
        uint frame = 0;
        uint animWait, wait;
        Point tune;

        public ImageSprite(string[] filenames, double width, Point fineTune, uint animationWait = 10)
        {
            sources = new ImageSource[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
                sources[i] = new BitmapImage(new Uri("pack://application:,,,/" + filenames[i]));

            _width = width;
            _height = sources[0].Height * _width / sources[0].Width;
            animWait = animationWait;
            tune = fineTune;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            Point lt = new Point(Parent.X + Parent.Width / 2 - _width / 2 + tune.X, Parent.Y + tune.Y);
            dc.DrawImage(sources[frame % sources.Length], new Rect(lt, new Size(_width, _height)));
            if (wait++ >= animWait)
            {
                frame++;
                wait = 0;
            }
        }
    }
}
