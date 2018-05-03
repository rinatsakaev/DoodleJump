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
        private bool isFalling;
        private int MaxAcceleration = 15;
        public void Move(Vector toPoint)
        {
            if (toPoint.Y != Coordinates.Y)
            {
                Acceleration = 0;
                isFalling = false;
            }

            if (!isFalling && Acceleration < MaxAcceleration)
                Acceleration++;
            if (Acceleration == MaxAcceleration)
                isFalling = true;
            if (isFalling)
                Acceleration--;
            Coordinates = new Vector(toPoint.X, toPoint.Y - Acceleration);
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public int Acceleration { get; private set; }

        public Player(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 1;
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\player.png");
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
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\greenplatform.png");
        }
    }

    public class BluePlatform : IObstacle
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

        public BluePlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 0;
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\blueplatform.png");
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
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\redplatform.png");
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
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\ufo.png");
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
            Image = Image.FromFile("C:\\Users\\Rinat\\source\\repos\\DoodleJump\\DoodleJump\\images\\bullet.png");
        }
    }
}
