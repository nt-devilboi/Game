﻿using System;
using System.Drawing;
using System.Numerics;
using System.Security;

namespace Model
{
    public class Physics
    {
        //скорость
        public float x { get; protected set; }
        public float y { get; protected set; }

        public PointF Velocity { get; set; }

        public float Mass { get; set; }

        //упругость
        public float Spring { get; set; }

        public bool Gravity { get; set; }

        public PointF Force { get; set; }


        public Physics()
        {
            Spring = 0.5f;
            Mass = 1;
            Gravity = true;
        }

        public virtual void Update(float dt, Player player, World world)
        {
            //сила
            var force = Force;
            Force = Point.Empty;
            //гравитация
            if (Gravity)
                force = new PointF(force.X, force.Y + 9.8f * Mass);
            //ускорение
            var ax = force.X / Mass;
            var ay = force.Y / Mass;
            //скорость
            Velocity = new PointF(Velocity.X + ax * dt, Velocity.Y + ay * dt);
            //координаты
            x = x + Velocity.X * dt;
            y = y + Velocity.Y * dt;
        }

        public void OnGroundCollision(float groundY)
        {
            if (Velocity.Y < -float.Epsilon) return;
            y = groundY - 0.0001f;
            Velocity = new PointF(Velocity.X, -Velocity.Y * Spring);
        }

        public void OnWallCollision(float groundX)
        {
            if (Velocity.X < -float.Epsilon) return;
            if (x < 0)
            {
                x = groundX - 0.0001f;
                Velocity = new PointF(-Velocity.X * Spring, Velocity.Y);
            }
            else
            {
                x = groundX + 0.0001f;
                Velocity = new PointF(Velocity.X * Spring, Velocity.Y);
            }
        }
    }
}