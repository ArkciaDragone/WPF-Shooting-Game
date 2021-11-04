using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Threading;

namespace tanmak.ProcessingBar
{
    class SimpleHealthBar : GameObject
    {
        int Ticks = 0;
        int NowHits;
        int MaxHP;
        Brush inBrush;
        Pen outPen;
        DispatcherTimer dispatcher;
        int StartHits;
        GameObject Boss;
        public SimpleHealthBar(World world, int MaxHP, GameObject Boss) : base(world)
        {
            this.Boss = Boss;
            this.MaxHP = MaxHP;
            inBrush = new SolidColorBrush(Color.FromArgb(180, 255, 0, 0));
            outPen = new Pen(Brushes.Black, 2);
        }
        public delegate void CALL_BACK();
        CALL_BACK DeathCall;
        public void SetEndCall(CALL_BACK c) => DeathCall = c;

        public void Activate()
        {
            StartHits = Boss.HitCount;
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(50);
            World.AddTopMost(this);
            dispatcher.Tick += delegate
            {
                NowHits = Boss.HitCount;
                if(NowHits-StartHits > MaxHP)
                {
                    Dead = true;
                    DeathCall?.Invoke();
                    dispatcher.Stop();
                }
                

            };
            dispatcher.Start();
        }
        public override void OnUpdate()
        {
            
        }
        public override void OnRender(DrawingContext dc)
        {
            double sx = 10, ex = World.Width - 10;
            double sy = 5, ey = 10;

            Rect rect = new Rect(sx, sy, (ex - sx + 1) * (1- (NowHits - StartHits)*1.0/MaxHP),
                ( ey - sy + 1));
            dc.DrawRectangle(inBrush, null, rect);
            rect = new Rect(sx, sy,ex - sx + 1, ey - sy + 1) ;
            dc.DrawRectangle(null, outPen, rect);
        }

    }
}
