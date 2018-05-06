﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
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
        private readonly Timer timer;
        //private readonly Image backgroundImage = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\bg.png");
        private readonly Image backgroundImage = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\bg.png");
        private HashSet<Type> allowedObjects = new HashSet<Type>();
        private Control lbl = new Label();
        public DoodleForm()
        {
            InitializeComponent();
            Controls.Add(lbl);
            DoubleBuffered = true;
            var level = new Level(GenerateMap, Height);
            timer = new Timer { Interval = 70 };
            timer.Tick += TimerTick;
            timer.Start();
        }


        private IEnumerable<IObstacle> GenerateMap()
        {
            var playerHeight = Level.Player.Coordinates.Y;
           
                allowedObjects.Add(typeof(GreenPlatform));
             
                 allowedObjects.Add(typeof(BluePlatform));
    
     
            var random = new Random();

            var type = allowedObjects.ElementAt(random.Next(allowedObjects.Count));

            yield return GetObstacleByType(type);

        }

        private IObstacle GetObstacleByType(Type type)
        {
            var rnd = new Random();
          
            var coordinates = new Vector(rnd.Next(0, Width), rnd.Next(0, Height));
            //var coordinates = new Vector(200, 60);
            var result = (IObstacle)Activator.CreateInstance(type, new object[] { coordinates });
            if (result is GreenPlatform)
            {
                result.Damage = 0;
                result.Health = 3;
            }

            if (result is RedPlatform)
            {
                result.Damage = 0;
                result.Health = 0;
            }

            if (result is UFO)
            {
                result.Damage = 3;
                result.Health = 2;
            }


            return result;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var angle = Math.PI / 2;
            if (right) angle = 0;
            else if (left) angle = Math.PI;

            Level.MoveObjects(angle, horizontalDistance);
            lbl.Text = "Player:" + Level.Player.Coordinates.X + " " + Level.Player.Coordinates.Y+"\n"+Level.Player.Acceleration;
            if (Level.IsCompleted)
                timer.Stop();
            Invalidate();
            Update();
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
                foreach (var element in Level.Map)
                {
                   
                    g.DrawImage(element.Image, new Point((int)(element.Coordinates.X - element.Image.Width / 2), (int)(element.Coordinates.Y - element.Image.Height / 2)));
                    g.DrawEllipse(new Pen(Color.Red), (int)element.Coordinates.X, (int)element.Coordinates.Y, 10, 10);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(1.0F, -1.0F);
            e.Graphics.TranslateTransform(0.0F, -(float)Height);
            e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            var g = Graphics.FromImage(backgroundImage);
           

            g.Clear(Color.AntiqueWhite);
            DrawTo(g);
            e.Graphics.DrawImage(backgroundImage, (ClientRectangle.Width - backgroundImage.Width) / 2, (ClientRectangle.Height - backgroundImage.Height) / 2);
        }
    }
}
