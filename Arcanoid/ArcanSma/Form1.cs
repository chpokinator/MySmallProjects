using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcanSma
{
    public partial class Form1 : Form
    {
        Game game;
        Form2 frm2;
        Form3 frm3;
        public List<PictureBox> bricks { get; set; }

        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            game = new Game(this, pictureBox1);
           
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            game.Update();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            game.MovePlatform(e.X);
        }
        
        public void DrawBricks()
        {
            bricks = new List<PictureBox>();
            int brickRows = 9;
            int countBricks = 8;
            int border = 3;
            int brickWidth = ClientSize.Width / countBricks - border;
            int brickHeight = 25;
            int x = border;
            int y = 0;

            for (int i = 0; i<brickRows;i++)
            {
                for(int j = 0; j<countBricks; j++)
                {
                    PictureBox brick = new PictureBox();
                    brick.Image = imageList1.Images[r.Next(imageList1.Images.Count)];
                    brick.Location = new Point(x, y);
                    brick.SizeMode = PictureBoxSizeMode.StretchImage;
                    brick.Width = brickWidth;
                    brick.Height = brickHeight;
                    this.Controls.Add(brick);
                    bricks.Add(brick);
                    x += ClientSize.Width / countBricks;

                }
                x = border;
                y += brickHeight + border;
            }

        }

        public void DeleteBrick(PictureBox a)
        {
            foreach(var b in Controls)
            {
                if(a == b as PictureBox)
                {
                    (b as PictureBox).Dispose();
                    bricks.Remove(a);
                }
            }
            this.Update();
            this.Invalidate();
        }

        public void Lose()
        {
            frm2 = new Form2();
            frm2.ShowDialog();
            this.Close();
            this.Dispose();
        }
        public void Won()
        {
            frm3 = new Form3();
            frm3.ShowDialog();
            this.Close();
            this.Dispose();
        }

        
    }
}
