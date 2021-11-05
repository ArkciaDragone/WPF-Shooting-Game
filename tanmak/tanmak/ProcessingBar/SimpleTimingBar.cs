using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using tanmak.BulletSkin;
using tanmak.Game;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace tanmak.ProcessingBar
{
    class SimpleTimingBar : GameObject
    {
        int EndTicks;
        int Ticks = 0;
        Brush inBrush;
        Pen outPen;
        DispatcherTimer dispatcher;
        ObjPlayer Player;
        public SimpleTimingBar(World world, int Ticks, ObjPlayer Player) : base(world)
        {
            this.Player = Player;
            EndTicks = Ticks;
            inBrush = new SolidColorBrush(Color.FromArgb(180, 0, 0, 255));
            outPen = new Pen(Brushes.Black, 2);
        }
        public delegate void CALL_BACK();
        CALL_BACK DeathCall;
        public void SetEndCall(CALL_BACK c) => DeathCall = c;

        public void Activate()
        {
            
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(50);
            Ticks = 0;
            World.AddTopMost(this);
            dispatcher.Tick += delegate
            {
                if (Player.Dead)
                    return;
                ++Ticks;
                if(Ticks >= EndTicks)
                {
                    Dead = true;
                    dispatcher.Stop();
                    DeathCall?.Invoke();
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

            Rect rect = new Rect(sx, sy, (ex - sx + 1) * (1- Ticks*1.0/EndTicks),
                ( ey - sy + 1));
            dc.DrawRectangle(inBrush, null, rect);
            rect = new Rect(sx, sy,ex - sx + 1, ey - sy + 1) ;
            dc.DrawRectangle(null, outPen, rect);
        }

    }
}
