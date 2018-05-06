using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    public class Player : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Health { get; set; }
        public bool isFalling { get; set; }
        public int MaxAcceleration = 15;
        public void Move(Vector toPoint)
        {
            if (isFalling)
                Acceleration--;
            if (!isFalling && Acceleration < MaxAcceleration)
                Acceleration++;
            if (Acceleration == MaxAcceleration)
                isFalling = true;
            Coordinates = new Vector(toPoint.X, toPoint.Y + Acceleration);
        }

        public void Jump()
        {
            isFalling = false;
            Acceleration = 15;
        }


        public Image Image { get; set; }
        public int Damage { get; set; }

        public int Acceleration { get; set; }

        public Player(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 1;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\player.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\player.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }
    }

    public class GreenPlatform : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            Coordinates = toPoint;
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public int Acceleration { get; }

        public GreenPlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 0;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\greenplatform.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\greenplatform.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }
    }

    public class BluePlatform : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Acceleration { get; private set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {

            if (isFalling&&Acceleration>-MaxAcceleration)
                Acceleration--;
            if (!isFalling && Acceleration < MaxAcceleration)
                Acceleration++;
            if (Acceleration == MaxAcceleration)
                isFalling = true;
            if (Acceleration == -MaxAcceleration)
                isFalling = false;
            Coordinates = new Vector(toPoint.X + Acceleration, toPoint.Y);
        }

        public Image Image { get; set; }
        public int Damage { get; set; }
        private bool isFalling = false;
        private int MaxAcceleration = 10;
        public BluePlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 0;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\blueplatform.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\blueplatform.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            Acceleration = 0;
        }
    }

    public class RedPlatform : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Acceleration { get; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            Coordinates = toPoint;
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public RedPlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 0;
            Damage = 0;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\redplatform.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\redplatform.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }
    }

    public class UFO : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Acceleration { get; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            Coordinates = toPoint;
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public UFO(Vector coordinates)
        {
            Coordinates = coordinates;
            Damage = 3;
            Health = 2;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\ufo.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\ufo.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }
    }

    public class Bullet : IObstacle
    {
        public Vector Coordinates { get; private set; }
        public int Acceleration { get; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            Coordinates = toPoint;
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public Bullet(Vector coordinates)
        {
            Coordinates = coordinates;
            Damage = 1;
            Health = 0;
            //Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\bullet.png");
            Image = Image.FromFile("C:\\Users\\Всеволод\\Documents\\ProgrammingStuff\\C#\\DoodleJump\\DoodleJump\\images\\bullet.png");
            Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }
    }
}
