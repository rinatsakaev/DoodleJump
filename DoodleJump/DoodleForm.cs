using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoodleJump
{
    public partial class DoodleForm : Form
    {
        private bool right;
        private bool left;
        private double horizontalDistance = 10;
        private double verticalDistance = 20;
        private readonly Timer timer;
        private readonly Image rocketImage = Image.FromFile("images/rocketImage.png");
        private readonly Image backgroundImage = Image.FromFile("images/bg.png");
        public DoodleForm()
        {
            InitializeComponent();
            Controls.Add(new Button());
            timer = new Timer { Interval = 10 };
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var angle = Math.PI/2;
            if (right) angle = 0;
            else if (left) angle = Math.PI;
      
            MovePlayer(angle);
            MoveObstacles();
            if (Level.IsCompleted)
                timer.Stop();
            Invalidate();
            Update();
        }


        private void MovePlayer(double angle)
        {
            Level.MovePlayer(angle, horizontalDistance);
        }

        private void MoveObstacles()
        {
            Level.MoveObstacles();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HandleKey(e.KeyCode, true);
        }

        private void HandleKey(Keys e, bool down)
        {
            if (e == Keys.A) left = down;
            if (e == Keys.D) right = down;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HandleKey(e.KeyCode, false);
        }

        private void DrawTo(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(Brushes.Beige, ClientRectangle);

            if (timer.Enabled)
            {
                foreach (var obstacle in Level.Obstacles)
                {
                    g.DrawImage(obstacle.Image, new Point((int)obstacle.Coordinates.X, (int)obstacle.Coordinates.Y));
                }
                g.DrawImage(rocketImage, new Point(-rocketImage.Width / 2, -rocketImage.Height / 2));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            var g = Graphics.FromImage(backgroundImage);
            DrawTo(g);
            e.Graphics.DrawImage(backgroundImage, (ClientRectangle.Width - backgroundImage.Width) / 2, (ClientRectangle.Height - backgroundImage.Height) / 2);
        }
    }
}
