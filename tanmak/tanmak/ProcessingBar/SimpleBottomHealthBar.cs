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
    class SimpleBottomHealthBar : GameObject
    {
        ScoreManager ScoreManager;
        int Ticks = 0;
        int NowHealth;
        int MaxHP;
        Brush inBrush;
        Pen outPen;
        DispatcherTimer dispatcher;
        int StartHits;
        GameObject Boss;
        public SimpleBottomHealthBar(World world, ScoreManager Sm) : base(world)
        {
            NowHealth = this.MaxHP = Sm.MaxHP;
            ScoreManager = Sm;
            inBrush = new SolidColorBrush(Color.FromArgb(180, 255, 0, 0));
            outPen = new Pen(Brushes.Black, 2);
        }
        public delegate void CALL_BACK();
        CALL_BACK DeathCall;
        public void SetEndCall(CALL_BACK c) => DeathCall = c;


        public override void OnUpdate()
        {
            NowHealth = ScoreManager.HP;
            if (NowHealth <= 0)
            {
                Dead = true;
                DeathCall?.Invoke();
            }
        }
        public override void OnRender(DrawingContext dc)
        {
            double sx = 10, ex = World.Width - 10;
            double sy = World.Height - 15, ey = World.Height - 10;

            Rect rect = new Rect(sx, sy, (ex - sx + 1) * ((NowHealth)*1.0/MaxHP),
                ( ey - sy + 1));
            dc.DrawRectangle(inBrush, null, rect);
            rect = new Rect(sx, sy,ex - sx + 1, ey - sy + 1) ;
            dc.DrawRectangle(null, outPen, rect);
        }

    }
}
