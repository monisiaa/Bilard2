using System;
using System.Linq;
using System.Windows;

namespace Bilard2
{
    public class SimulationBox
    {
        private readonly Ball[] balls;
        private readonly Table table;

        public SimulationBox(Table table, Ball[] balls)
        {
            this.table = table;
            this.balls = balls;
        }

        public void UpdateSimulation(double dt)
        {
            var activeBalls = balls.Where(b => b.IsVisible).ToList();

            while(true)
            {
                var collisions = false;

                foreach(var ball in activeBalls)
                {
                    if(HandleCollisionsWithWalls(ball))
                    {
                        collisions = true;
                    }
                }

                for(int i = 0; i < activeBalls.Count; i++)
                {
                    for(int j = i + 1; j < activeBalls.Count; j++)
                    {
                        if(HandleBallsCollision(activeBalls[i], activeBalls[j], dt))
                        {
                            collisions = true;
                        }
                    }
                }

                if(!collisions)
                {
                    break;
                }
            }
        }

        private bool HandleCollisionsWithWalls(Ball ball)
        {
            if(ball.Center.X - ball.R < table.X + table.Edge)
            {
                ball.Velocity = new Vector(-ball.Velocity.X, ball.Velocity.Y);
                ball.Center = new Vector(table.X + table.Edge + ball.R, ball.Center.Y);
                return true;
            }

            if(ball.Center.X + ball.R > table.X + table.W - table.Edge)
            {
                ball.Velocity = new Vector(-ball.Velocity.X, ball.Velocity.Y);
                ball.Center = new Vector(table.X + table.W - table.Edge - ball.R, ball.Center.Y);
                return true;
            }

            if (ball.Center.Y - ball.R < table.Y + table.Edge)
            {
                ball.Velocity = new Vector(ball.Velocity.X, -ball.Velocity.Y);
                ball.Center = new Vector(ball.Center.X, table.Y + table.Edge + ball.R);
                return true;
            }

            if (ball.Center.Y + ball.R > table.Y + table.H - table.Edge)
            {
                ball.Velocity = new Vector(ball.Velocity.X, -ball.Velocity.Y);
                ball.Center = new Vector(ball.Center.X, table.Y + table.H - table.Edge - ball.R);
                return true;
            }

            return false;
        }

        private bool HandleBallsCollision(Ball A, Ball B, double dt)
        {
            // jeżeli kule kolidują, t przyjmie wartości od 0..1
            if(!AreBallsColliding(A, B, dt, out var t))
            {
                return false;
            }

            // kule się zderzą po zaaplikowaniu ich prędkości pomnożonej przez t
            A.Center += A.Velocity * t * dt;
            B.Center += B.Velocity * t * dt;

            // reakcja na zderzenie
            HandleBallsCollisionReaction(A, B);

            // na kule po zderzeniu należy jeszcze zaaplikować nową prędkość pomnożoną przez (1 - t)
            A.Center += A.Velocity * (1.0 - t) * dt;
            B.Center += B.Velocity * (1.0 - t) * dt;
            return true;
        }
        
        private bool AreBallsColliding(Ball A, Ball B, double dt, out double t)
        {
            // http://www.gamasutra.com/view/feature/131424/pool_hall_lessons_fast_accurate_.php?page=2
            var V = (A.Velocity - B.Velocity) * dt;
            var radiusSum = B.R + A.R;
            var ballsDistance = (B.Center - A.Center).Length - radiusSum;
            t = 0;

            // kule są zbyt odległe by mogły ze sobą kolidować
            if(V.Length < ballsDistance)
            {
                return false;
            }

            var N = V; N.Normalize();

            var C = B.Center - A.Center;
            var D = Vector.Multiply(N, C);

            // kula A nie porusza się w stronę kuli B
            if(D <= 0)
            {
                return false;
            }

            var F = C.LengthSquared - D * D;
            var radiusSumSquared = radiusSum * radiusSum;

            if (F >= radiusSumSquared)
            {
                return false;
            }

            var T = radiusSumSquared - F;

            if(T < 0)
            {
                return false;
            }

            var distance = D - Math.Sqrt(T);
            
            if(V.Length < distance)
            {
                return false;
            }

            t = (N * distance).Length / V.Length;
            return true;
        }

        private void HandleBallsCollisionReaction(Ball A, Ball B)
        {
            var N = A.Center - B.Center;
            N.Normalize();

            var v1 = A.Velocity;
            var v2 = B.Velocity;

            // a1 =  v1 . N
            var a1 = Vector.Multiply(v1, N);

            // a2 = v2 . N
            var a2 = Vector.Multiply(v2, N);

            var m1 = A.Mass;
            var m2 = B.Mass;
            var p = 2 * (a1 - a2) / (m1 + m2);

            var newV1 = v1 - p * m2 * N;
            var newV2 = v2 + p * m1 * N;

            A.Velocity = newV1;
            B.Velocity = newV2;
        }
    }
}
