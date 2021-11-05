using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace tanmak.Engine
{
    public abstract class World
    {
        public List<GameObject> UnderObjects = new List<GameObject>();
        public List<GameObject> UnderPadding = new List<GameObject>();

        public List<GameObject> Objects = new List<GameObject>();
        public List<GameObject> PaddingObjects = new List<GameObject>();

        public List<GameObject> TopMostObjects = new List<GameObject>();
        public List<GameObject> TopMostPadding = new List<GameObject>();

        public List<GameObject> TopTopMostObjects = new List<GameObject>();
        public List<GameObject> TopTopMostPadding = new List<GameObject>();

        public double Width { get; set; }

        public double Height { get; set; }

        public GamePlane Plane { get; set; }

        public World(GamePlane Plane)
        {
            this.Plane = Plane;
            Width = Plane.ActualWidth;
            Height = Plane.ActualHeight;
            var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += CollectAll;
            timer.Start();
        }
        public virtual void OnRender(DrawingContext dc)
        {
            foreach (GameObject obj in UnderObjects)
                if (!obj.Dead)
                    obj.OnRender(dc);
            foreach (GameObject obj in Objects)
            {
                if (!obj.Dead)
                    if (obj.X >= -obj.Width*2 && obj.X <= Width + obj.Width*2 && 
                        obj.Y >= 0 - obj.Height*2 && obj.Y <= Height + obj.Height*2)
                    {
                        obj.OnRender(dc);
                    }
            }
            foreach (GameObject obj in TopMostObjects)
                if (!obj.Dead)
                    obj.OnRender(dc);
            foreach (GameObject obj in TopTopMostObjects)
                if (!obj.Dead)
                    obj.OnRender(dc);
        }

        public virtual void OnUpdate()
        {
            Width = Plane.ActualWidth;
            Height = Plane.ActualHeight;
            ProcessPaddingObjects();

            foreach (GameObject obj in UnderObjects)
                if (!obj.Dead)
                    obj.OnUpdate();
            foreach (GameObject obj in Objects)
                if (!obj.Dead)
                    obj.OnUpdate();
            foreach (GameObject obj in TopMostObjects)
                if (!obj.Dead)
                    obj.OnUpdate();
            foreach (GameObject obj in TopTopMostObjects)
                if (!obj.Dead)
                    obj.OnUpdate();

            ProcessPaddingObjects(true);
        }

        private void CollectAll(object _, EventArgs __)
        {
            GarbageCollection(ref UnderObjects);
            GarbageCollection(ref Objects);
            GarbageCollection(ref TopMostObjects);
            GarbageCollection(ref TopTopMostObjects);
        }

        internal void ProcessPaddingObjects(bool doUpdate = false)
        {
            if (doUpdate)
            {
                foreach (GameObject obj in UnderPadding)
                    obj.OnUpdate();
                foreach (GameObject obj in PaddingObjects)
                {
                    obj.OnUpdate();
                }
                foreach (GameObject obj in TopMostPadding)
                    obj.OnUpdate();
                foreach (GameObject obj in TopTopMostPadding)
                    obj.OnUpdate();
            }

            if (UnderPadding.Count > 0)
            {
                UnderObjects.AddRange(UnderPadding);
                UnderPadding.Clear();
            }
            if (PaddingObjects.Count > 0)
            {
                Objects.AddRange(PaddingObjects);
                PaddingObjects.Clear();
            }
            if (TopMostPadding.Count > 0)
            {
                TopMostObjects.AddRange(TopMostPadding);
                TopMostPadding.Clear();
            }
            if (TopTopMostPadding.Count > 0)
            {
                TopTopMostObjects.AddRange(TopTopMostPadding);
                TopTopMostPadding.Clear();
            }
        }

        public void AddObject(GameObject obj)
        {
            PaddingObjects.Add(obj);
        }
        public void AddTopMost(GameObject obj)
        {
            TopMostPadding.Add(obj);
        }
        public void AddTopTopMost(GameObject obj)
        {
            TopTopMostPadding.Add(obj);
        }

        public void AddUnder(GameObject obj)
        {
            UnderPadding.Add(obj);
        }

        public void DrawText(DrawingContext dc, string text = "", double x = 0, double y = 0, double size = 12, HorizontalAlignment ha = HorizontalAlignment.Left, VerticalAlignment va = VerticalAlignment.Top)
        {
            FormattedText ft = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, Defaults.Typeface, size, Brushes.Black);
            double xOffset = 0;
            switch (ha)
            {
                case HorizontalAlignment.Center:
                    xOffset = -ft.Width / 2;
                    break;
                case HorizontalAlignment.Right:
                    xOffset = -ft.Width;
                    break;
            }
            double yOffset = 0;
            switch (va)
            {
                case VerticalAlignment.Center:
                    yOffset = -ft.Height / 2;
                    break;
                case VerticalAlignment.Bottom:
                    yOffset = -ft.Height;
                    break;
            }
            dc.DrawText(ft, new Point(Math.Round(x + xOffset), Math.Round(y + yOffset)));
        }

        internal void GarbageCollection(ref List<GameObject> Objects_)
        {
            List<GameObject> Buf = new List<GameObject>();
            foreach (GameObject a in Objects_)
                if (!a.Dead)
                    Buf.Add(a);
            Objects_ = Buf;
        }
    }
}
