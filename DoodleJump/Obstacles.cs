using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    public class Player:IObstacle
    {
        public Vector Coordinates { get; set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            throw new NotImplementedException();
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public Player(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 1;
        }
    }

    public class GreenPlatform : IObstacle
    {
        public Vector Coordinates { get; set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            throw new NotImplementedException();
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public GreenPlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 3;
            Damage = 0;
        }
    }

    public class RedPlatform : IObstacle
    {
        public Vector Coordinates { get; set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            throw new NotImplementedException();
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public RedPlatform(Vector coordinates)
        {
            Coordinates = coordinates;
            Health = 0;
            Damage = 0;
        }
    }

    public class UFO : IObstacle
    {
        public Vector Coordinates { get; set; }
        public int Health { get; set; }
        public void Move(Vector toPoint)
        {
            throw new NotImplementedException();
        }

        public Image Image { get; set; }
        public int Damage { get; set; }

        public UFO(Vector coordinates)
        {
            Coordinates = coordinates;
            Damage = 3;
            Health = 2;
        }
    }

    public class Bullet:IObstacle
    {
        public Vector Coordinates { get; set; }
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
        }
    }
}
