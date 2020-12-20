using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcanSma
{
    class Game
    {
        Graphics graphics;
        Form1 frm;
        Ball ball;
        PictureBox platform;

        System.Threading.Timer timer;
        public Game(Form1 form, PictureBox platform)
        {
            this.frm = form;
            Random r = new Random();

            ball = new Ball
            {
                X = frm.ClientSize.Width / 2,
                Y = 300,
                Radius = 10,
                DX = 2,
                DY = 10
            };
            this.platform = platform;


            frm.DrawBricks();

            StartTimer();
        }

        private void UpdateGame()
        {
            if (!CheckBricksLeft())
                frm.Won();
            if (CheckBrickYCollision(ball))
                ball.DY = -ball.DY;
            if (CheckBrickXCollision(ball))
                ball.DX = -ball.DX;
            if (CheckWallXCollision(ball))
                ball.DX = -ball.DX;
            if (CheckWallYCollision(ball))
                ball.DY = -ball.DY;
            if (CheckPlatformCollision(ball))
                ball.DY = -ball.DY;



            ball.X += ball.DX;
            ball.Y += ball.DY;

            frm.Invalidate();


        }

        private void StartTimer()
        {
            timer = new System.Threading.Timer(x => UpdateGame(), null, 0, 15);
        }

        private bool CheckWallYCollision(Ball ball)
        {
            if (ball.Y + ball.Radius * 2 >= frm.ClientRectangle.Bottom)
            {
                timer.Dispose();
                frm.Lose();
            }
            if (ball.Y < frm.ClientRectangle.Top)
            {

                return true;
            }
               

            return false;
        }
        private bool CheckWallXCollision(Ball ball)
        {
            if (ball.X + ball.Radius * 2 >= frm.ClientRectangle.Right)
                return true;
            if (ball.X < frm.ClientRectangle.Left)
                return true;

            return false;
        }

        private bool CheckPlatformCollision(Ball ball)
        {
            if ((ball.X + ball.Radius >= platform.Location.X) && (ball.X + ball.Radius <= platform.Location.X + platform.Width))
            {
                if (ball.Y + ball.Radius * 2 > platform.Top)
                {
                    return true;
                }
            }


            return false;
        }

        private bool CheckBrickYCollision(Ball ball)
        {
            foreach (var a in frm.bricks)
            {
                if (((ball.X + ball.Radius) >= a.Location.X) && ((ball.X + ball.Radius) <= a.Location.X + a.Width))
                {
                    if (ball.Y + ball.Radius * 2 - 18 < a.Bottom)
                    {
                        frm.DeleteBrick(a);
                        return true;
                    }
                }




            }
            return false;
        }

        private bool CheckBrickXCollision(Ball ball)
        {
            foreach (var a in frm.bricks)
            {
                if ((ball.Y + ball.Radius >= a.Location.Y) && (ball.Y + ball.Radius <= a.Location.Y + a.Height))
                {
                    if ((ball.X + ball.Radius * 2 >= a.Location.X) && (ball.X + ball.Radius*2 < a.Location.X + a.Width))
                    {
                        frm.DeleteBrick(a);
                        return true;
                    }

                    if ((ball.X + ball.Radius * 2 >= a.Location.X + a.Width) && (ball.X + ball.Radius * 2 < a.Location.X))
                    {
                        frm.DeleteBrick(a);
                        return true;
                    }

                }


            }

            return false;
        }

        private bool CheckBricksLeft()
        {
            if (frm.bricks.Count > 0)
                return true;
            else
            {
                timer.Dispose();
                return false;
            }
                
           
        }

        private void DrawBall(Ball ball)
        {
            graphics.FillEllipse(Brushes.Brown, ball.X,
                                               ball.Y,
                                               ball.Radius * 2,
                                               ball.Radius * 2);
        }

        public void MovePlatform(int x)
        {
            platform.Location = new Point(x, platform.Location.Y);
        }

        public void Update()
        {
            graphics = frm.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DrawBall(ball);
        }





    }
}
