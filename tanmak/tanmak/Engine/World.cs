﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace tanmak.Engine
{
    public abstract class World
    {
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
        }
        public virtual void OnRender(DrawingContext dc)
        {
            foreach (GameObject obj in Objects)
            {
                if (!obj.Dead)
                    if(obj.X >= 0-30 && obj.X < Width+30 && obj.Y >= 0-30 && obj.Y <= Height+30)
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

            foreach (GameObject obj in Objects)
            {
                if (!obj.Dead)
                {
                    obj.OnUpdate();
                }
            }
            foreach (GameObject obj in TopMostObjects)
                if(!obj.Dead)
                    obj.OnUpdate();
            foreach (GameObject obj in TopTopMostObjects)
                if (!obj.Dead)
                    obj.OnUpdate();
            GarbageCollection();
            GarbageCollection_(ref TopMostObjects);
            GarbageCollection_(ref TopTopMostObjects);
            ProcessPaddingObjects(true);
        }

        internal void ProcessPaddingObjects(bool doUpdate = false)
        {
            if (doUpdate)
            {
                foreach (GameObject obj in PaddingObjects)
                {
                    obj.OnUpdate();
                }
                foreach (GameObject obj in TopMostPadding)
                    obj.OnUpdate();
                foreach (GameObject obj in TopTopMostPadding)
                    obj.OnUpdate();
            }

            if (PaddingObjects.Count > 0)
            {
                Objects.AddRange(PaddingObjects);
                PaddingObjects.Clear();
            }
            if(TopMostPadding.Count>0)
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

        internal void GarbageCollection()
        {
            GarbageCollection_(ref Objects);
        }
        internal void GarbageCollection_(ref List<GameObject> Objects_)
        {
            int index = 0;
            while (true)
                if (index >= Objects_.Count)
                    break;
                else
                    if (Objects_[index].Dead)
                    Objects_.RemoveAt(index);
                else
                    index++;
        }
    }
}
