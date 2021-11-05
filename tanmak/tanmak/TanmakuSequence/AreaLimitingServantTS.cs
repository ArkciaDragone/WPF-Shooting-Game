using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tanmak.Engine;
using System.Windows.Threading;
using tanmak.BulletSkin;
using tanmak.Game;
using tanmak.Servant;
using System.Windows;
using System.Windows.Media;

namespace tanmak.TanmakuSequence
{

    public class SingleAreaLimitingServantTanmakuSequence:EmptyTanmakuSequence
    {
        public class TickerObject:GameObject
        {

            public delegate void CALL_BACL();
            CALL_BACK DeathCall;

            int Ticks;
            public TickerObject(World world, int Tks, CALL_BACK Cb):base(world)
            {
                DeathCall = Cb;
                Ticks = Tks;
            }
            public override void OnUpdate()
            {
                if(Ticks == 0)
                {
                    if(!Dead)
                       DeathCall?.Invoke();
                    Dead = true;
                }    
                --Ticks;
            }
            public override void OnRender(DrawingContext dc)
            {
            }
        };

        CALL_BACK MidTerm;

        int ShootInter;
        double ShootV, ShootDx;
        EmptyBulletSkin Skin;
        bool IsActivate;
        int Wait;
        public SingleAreaLimitingServantTanmakuSequence(World world, GameObject parent,
            EmptyBulletSkin Skin,
            int FirstShootrs, int ShootInterv, double ShootV, double ShootDx,
            CALL_BACK Mid=null):base(world, parent)
        {
            MidTerm = Mid;
            this.ShootInter = ShootInterv;
            this.ShootV = ShootV;
            this.ShootDx = ShootDx;
            Wait = FirstShootrs;

            dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromMilliseconds(50);
            dispatcher.Tick += delegate
            {
                OnUpdate();
            };
            this.Skin = Skin;
            EndTick = 9999;
        }
        int Ticks = 0;

        override public void Activate()
        {
            IsActivate = false;
            Ticks = 0;
            dispatcher.Start();
            World.AddObject(new TickerObject(World, Wait, delegate
            {
                IsActivate = true;
                MidTerm?.Invoke();
            }));
        }

        public void OnUpdate()
        {
            if (!IsActivate)
                return;
            if((Ticks)%ShootInter == 0)
            {
                double dx = Parent.X - World.Width / 2 + Parent.Width/2;
                double dy = Parent.Y - World.Height / 2 + Parent.Height/2;
                double ang = Math.Atan2(dy, dx) + this.ShootDx;
                double vx = Math.Cos(ang) * this.ShootV;
                double vy = Math.Sin(ang) * this.ShootV;
                World.AddObject(new DirectedLinearBullet(Skin, World,
                    Parent.X + Parent.Width/2, Parent.Y + Parent.Height/2,
                    vx, vy));
            }
            ++Ticks;
        }
    };

    public class AreaLimitingServantTanmakuSequence: EmptyTanmakuSequence
    {

        int Len1 = 200;
        int Len2 = 400;
        double R1 = 400;
        double R2 = 200;
        double rV = 2.24;
        NormalServant[] Servants;
        int Called = 0;
        public double RFunc(int Tick)
        {
            if (Tick <= Len1)
                return R1 * (Tick * 1.0 / Len1);
            else if ((Tick -= Len1) <= Len2)
                return R1 + (R2 - R1) * (2 - (2 / ((Tick * 1.0 / Len2) + 1)));
            else
                return R2;
        }

        public double AFunc(int Tick)
        {
            return ((rV * Tick) % 360) * Math.PI / 180.0;
        }

        CALL_BACK MidTerm;
        
        public AreaLimitingServantTanmakuSequence(World world, GameObject parent,GameObject Player,
            EmptyBulletSkin Ska = null, int Numbers = 4,
            int Shoot_Inter=1, double Shoot_V = 1.2, double Shoot_Angle_d = 0.9,
            CALL_BACK Mid = null) :
            base(world,parent)
        {
            MidTerm = Mid;
            Servants = new NormalServant[Numbers];
            int wi;

            if (Ska == null)
                Ska = new SimplePurpleBigTamaSkin();

            string[] Fn = { "Sources/Halos/PurpleHalo.png" };
            ImageSprite Sprite = new Engine.ImageSprite(Fn, 70, 70);
            ((ImageSprite)Sprite).SetAngularSpeed(5);

            for (wi=0;wi< Numbers;++wi)
            {
                int ani = new int();
                ani = wi;
                Point PFunc(int T)
                {
                    double r = RFunc(T);
                    double v = AFunc(T);
                    v += (2 * Math.PI / Numbers) * ani;

                    return new Point(r * Math.Cos(v) + World.Width/2, r * Math.Sin(v) + World.Height/2);
                }
                Servants[wi] = new NormalServant(World, Player, Parent,
                    PFunc, Sprite);
                var a = new SingleAreaLimitingServantTanmakuSequence(World, Servants[wi], Ska,
                    Len1, Shoot_Inter, Shoot_V, Shoot_Angle_d, delegate
                    {
                        if (0!=Called)
                            MidTerm?.Invoke();
                        ++Called;
                    });
                Servants[wi].SetSequence(a);
            }
            EndTick = 999999;
        }

        public override void Activate()
        {

            ((Eevees)Parent).MoveTo(World.Width / 2, World.Height / 2, 400);
            for (int wi = 0; wi < Servants.Length; ++wi)
                Servants[wi].Activate();
        }

        public override void Stop()
        {
            foreach (NormalServant a in Servants)
                a.Stop();
        }

        public override Point GetMovingTo()
        {
            return new Point(World.Width / 2 - Parent.Width / 2, World.Height / 2 - Parent.Width / 2);
        }

    }
}
