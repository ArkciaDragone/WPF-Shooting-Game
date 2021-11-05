using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace tanmak.Engine
{
    public class ImageSprite : Sprite
    {
        double _width, _height, currentAngle, _alpha = 1;
        ImageSource[] sources;
        uint frame = 0;
        uint animWait, wait;
        Point tune;
        double? angSpeed = null;
        private static Dictionary<string, ImageSource> imageCache = new Dictionary<string, ImageSource>();
        ImageSprite()
        {

        }

        public ImageSprite(string[] filenames, double width = 0, double height = 0, Point fineTune = new Point(), uint animationWait = 10)
        {
            sources = new ImageSource[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
            {
                try
                {
                    sources[i] = imageCache[filenames[i]];
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("cache miss with {0}", filenames[i]);
                    imageCache[filenames[i]] = sources[i] = new BitmapImage(new Uri("pack://application:,,,/" + filenames[i]));;
                }
            }

            if (width < 0 || height < 0 || (width == 0 && height == 0))
            {
                _width = sources[0].Width;
                _height = sources[0].Height;
            }
            else
            {
                if (height == 0)
                {
                    _height = sources[0].Height * width / sources[0].Width;
                    _width = width;
                }
                else if (width == 0)
                {
                    _height = height;
                    _width = sources[0].Width * _height / sources[0].Height;
                }
                else
                {
                    _height = height;
                    _width = width;
                }
            }
            animWait = animationWait;
            tune = fineTune;
        }
        public bool IsRotate()
        {
            return (Math.Abs(angSpeed??0) > 0.001);
        }
        public override void Render(GameObject Parent, DrawingContext dc)
        {
            Point lt = new Point(Parent.X + tune.X, Parent.Y + tune.Y);
            Point center = new Point(lt.X + _width / 2, lt.Y + _height / 2);

            currentAngle += angSpeed ?? 0;
            if(Math.Abs(currentAngle) > 0.01)
                dc.PushTransform(new RotateTransform(currentAngle, center.X, center.Y));
            if(Math.Abs(_alpha)< 0.99)
                dc.PushOpacity(_alpha);
            dc.DrawImage(sources[frame % sources.Length], new Rect(lt, new Size(_width, _height)));
            if (Math.Abs(_alpha) < 0.99)
                dc.Pop();
            if (Math.Abs(currentAngle) > 0.01)
                dc.Pop();

            if (wait++ >= animWait)
            {
                frame++;
                wait = 0;
            }
        }

        public void SetAngularSpeed(double speed) => angSpeed = speed;
        public void SetAngle(double angle) => currentAngle = angle;
        public void SetAlpha(double alpha)
        {
            if (alpha >= 0 && alpha <= 1) _alpha = alpha;
            else throw new ArgumentOutOfRangeException(nameof(alpha));
        }
        public int GetHeight() => (int)_height;
        public int GetWidth() => (int)_width;

        public ImageSprite Clone()
        {
            ImageSprite ret = new ImageSprite();
            ret.sources = sources;
            ret.tune = tune;
            ret.wait = wait;
            ret._alpha = _alpha;
            ret._height = _height;
            ret._width = _width;
            ret.angSpeed = angSpeed;
            ret.animWait = animWait;
            ret.currentAngle = currentAngle;
            ret.frame = frame;
            return ret;
        }
    }
}
