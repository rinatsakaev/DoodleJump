using System;
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
        private readonly Image backgroundImage = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\bg.png");
        private HashSet<Type> allowedObjects = new HashSet<Type>();
        private Control lbl = new Label();
        public DoodleForm()
        {
            InitializeComponent();
            Controls.Add(lbl);
          
            var level = new Level(GenerateMap, Height);
            timer = new Timer { Interval = 400 };
            timer.Tick += TimerTick;
            timer.Start();
        }


        private IEnumerable<IObstacle> GenerateMap()
        {
            if (Level.LevelHeight < 100)
                allowedObjects.Add(typeof(GreenPlatform));
            if (Level.LevelHeight < 500)
                allowedObjects.Add(typeof(RedPlatform));
            if (Level.LevelHeight > 1000)
                allowedObjects.Add(typeof(UFO));
            var random = new Random();

            var type = allowedObjects.ElementAt(random.Next(allowedObjects.Count));

            yield return GetObstacleByType(type);

        }

        private IObstacle GetObstacleByType(Type type)
        {
            var rnd = new Random();
            var coordinates = new Vector(rnd.Next(0, Width), rnd.Next(Level.LevelHeight, Height+Level.LevelHeight));
            var result = (IObstacle)Activator.CreateInstance(type, new object[]{coordinates});
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
            lbl.Text = Level.LevelHeight.ToString();
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
                var currentElement = Level.Map.Head;
                for (var i = 0; i < Level.Map.Count; i++)
                {
                    g.DrawImage(currentElement.Value.Image, new Point((int)currentElement.Value.Coordinates.X, (int)currentElement.Value.Coordinates.Y%Height));
                    currentElement = currentElement.Next;
                }
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
