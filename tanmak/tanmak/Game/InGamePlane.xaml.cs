﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using tanmak.Engine;

namespace tanmak.Game
{
    /// <summary>
    /// InGamePlane.xaml
    /// </summary>
    public partial class InGamePlane : UserControl, GamePlaneControl
    {
        public InGamePlane()
        {
            InitializeComponent();

            Loaded += InGamePlane_Loaded;
        }

        private void InGamePlane_Loaded(object sender, RoutedEventArgs e)
        {
            plane.World = new TanmakWorld(plane);
        }

        private void Bt_Restart_Click(object sender, RoutedEventArgs e)
        {
            plane.World = new TanmakWorld(plane);
        }

        public void SetTransfrom(Transform transfrom)
        {
            RenderTransform = transfrom;
        }

        public void SetTransfromOrigin(Point pt)
        {
            RenderTransformOrigin = pt;
        }
    }
}
