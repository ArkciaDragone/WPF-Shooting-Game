﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace tanmak.Engine
{
    public abstract class GameObject : DependencyObject
    {
        public DispatcherTimer timer;
        public virtual void MoveAway()
        {
            if (timer.IsEnabled)
                timer.Stop();
            MoveTo(World.Width/2 - Width /2, -250, 1000);
        }

        public int HitCount;
        public static DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(GameObject));
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(GameObject));
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public Sprite Sprite { get; set; }

        public World World { get; set; }

        public bool Dead { get; set; } = false;

        public GameObject(World world)
        {
            World = world;
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnRender(DrawingContext dc)
        {
            if (Sprite != null)
                Sprite.Render(this, dc);
        }

        private Storyboard MoveToStoryboard;
        public Storyboard MoveTo(double x, double y, double durationMs)
        {
            if (MoveToStoryboard != null)
            {
                MoveToStoryboard.Stop();
                MoveToStoryboard.Remove();
            }

            Storyboard sb = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(durationMs));

            DoubleAnimation xani = new DoubleAnimation(x, duration);
            DoubleAnimation yani = new DoubleAnimation(y, duration);

            Storyboard.SetTargetProperty(xani, new PropertyPath(XProperty));
            Storyboard.SetTargetProperty(yani, new PropertyPath(YProperty));

            Storyboard.SetTarget(xani, this);
            Storyboard.SetTarget(yani, this);

            sb.Children.Add(xani);
            sb.Children.Add(yani);

            sb.Begin();

            MoveToStoryboard = sb;

            return sb;
        }

        public static bool IsHit(GameObject me, GameObject other)
        {
            double tx = me.X + me.Width / 2;
            double ty = me.Y + me.Height / 2;
            double mx = other.X + other.Width / 2;
            double my = other.Y + other.Height / 2;
            double d2 = (tx - mx) * (tx - mx) + (ty - my) * (ty - my);
            d2 -= 2;

            return d2 <= other.Width * other.Width / 4 && d2 < other.Height * other.Height / 4;

            return other.X >= me.X - other.Width && other.X <= me.X + me.Width &&
                other.Y >= me.Y - other.Height && other.Y <= me.Y + me.Height;
        }

        public bool IsHit(GameObject other) => IsHit(this, other);

        public void CheckOutOfBounds()
        {
            if (X < -World.Width || X > World.Width + Width || Y < -World.Height || Y > World.Height + Height)
            {
                Dead = true;
            }
        }
    }
}