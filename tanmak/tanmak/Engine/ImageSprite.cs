using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace tanmak.Engine
{
    public class ImageSprite : Sprite
    {
        double _width, _height, currentAngle;
        ImageSource[] sources;
        uint frame = 0;
        uint animWait, wait;
        Point tune;
        double? angSpeed = null;

        public ImageSprite(string[] filenames, double width, double height = 0, Point fineTune = new Point(), uint animationWait = 10)
        {
            sources = new ImageSource[filenames.Length];
            for (int i = 0; i < filenames.Length; i++)
                sources[i] = new BitmapImage(new Uri("pack://application:,,,/" + filenames[i]));

            if (width < 0 || height < 0)
            {
                _width = sources[0].Width;
                _height = sources[0].Height;
            }
            else
            {
                _width = width;
                if (height == 0) _height = sources[0].Height * _width / sources[0].Width;
            }
            animWait = animationWait;
            tune = fineTune;
        }

        public override void Render(GameObject Parent, DrawingContext dc)
        {
            Point lt = new Point(Parent.X + tune.X, Parent.Y + tune.Y);
            Point center = new Point(lt.X + _width / 2, lt.Y + _height / 2);

            currentAngle += angSpeed ?? 0;
            dc.PushTransform(new RotateTransform(currentAngle, center.X, center.Y));
            dc.DrawImage(sources[frame % sources.Length], new Rect(lt, new Size(_width, _height)));
            dc.Pop();

            if (wait++ >= animWait)
            {
                frame++;
                wait = 0;
            }
        }

        public void SetAngularSpeed(double speed) => angSpeed = speed;
        public void SetAngle(double angle) => currentAngle = angle;
    }
}
