using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    class Level
    {
        public static bool IsCompleted;
        public static Player Player => GetPlayer();
        public static LinkedList<IObstacle> Map;
        public static Dictionary<string, Action<IObstacle>> moves;
        private static double VerticalDistance = 15;
        private static int ScreenHeight;
        private static Func<IEnumerable<IObstacle>> MapGenerator;
        private static bool hasToJump = false;

        private static Player GetPlayer()
        {
            var player = (Player)Map.Where(e => e is Player).FirstOrDefault();
            return player ?? throw new Exception();
        }

        public Level(Func<IEnumerable<IObstacle>> mapGenerator, int screenHeight)
        {
            ScreenHeight = screenHeight;
            MapGenerator = mapGenerator;
            Map = new LinkedList<IObstacle>();
            Map.AddFirst(new Player(new Vector(200, 60)));
            Map.AddLast(new GreenPlatform(new Vector(200, 50)));
            AddNewObjectsToMap();
            InitializeMoves();
        }

        private void InitializeMoves()
        {
            moves = new Dictionary<string, Action<IObstacle>>();
            moves["GreenPlatform"] = x => MoveGreenPlatform((GreenPlatform)x);
            moves["RedPlatform"] = x => MoveRedPlatform((RedPlatform)x);
            moves["UFO"] = x => MoveUFO((UFO)x);
            moves["Bullet"] = x => MoveBullet((Bullet)x);
            moves["BluePlatform"] = x => MoveBluePlatform((BluePlatform)x);
        }

        private static void MovePlayer(double angle, double distance)
        {
            Player.Move(new Vector(Player.Coordinates.X + distance * Math.Cos(angle), Player.Coordinates.Y));

            if (Player.Coordinates.Y >= ScreenHeight / 2 && Player.Acceleration > 0)
            {
                foreach (var element in Map)
                {
                    if (element is Player)
                        continue;
                    element.Move(new Vector(element.Coordinates.X, element.Coordinates.Y - Player.Acceleration));
                }
            }
        }

        public static void MoveObjects(double playerAngle, double distance)
        {
            MovePlayer(playerAngle, distance);
            foreach (var element in Map)
            {
                if (element is Player)
                    continue;
                moves[element.GetType().Name](element);
            }
            UpdateMap();
        }

        private void MoveGreenPlatform(GreenPlatform platform)
        {
        }

        private void MoveRedPlatform(RedPlatform platform)
        {
        }

        private void MoveUFO(UFO ufo)
        {
        }

        private void MoveBullet(Bullet bullet)
        {
            bullet.Move(new Vector(bullet.Coordinates.X, bullet.Coordinates.Y - 1));
        }
        private void MoveBluePlatform(BluePlatform platform)
        {
            platform.Move(new Vector(platform.Coordinates.X, platform.Coordinates.Y));
        }


        public static void UpdateMap()
        {

            foreach (var element in Map)
            {
                if (element is Player)
                    continue;
                if (Math.Abs(Player.Coordinates.Y - element.Coordinates.Y) <= element.Image.Height + 10
                    && Math.Abs(Player.Coordinates.X - element.Coordinates.X) <= element.Image.Width / 2 + 5 && Player.isFalling)
                {
                    Player.Jump();
                    break;
                }
            }

            RemoveOldObjectsFromMap();

            if (Player.Health == 0 || Map.FindMinElement() is Player && Player.Acceleration <= 0)
                IsCompleted = true;

            AddNewObjectsToMap();

        }

        private static void AddNewObjectsToMap()
        {
            foreach (var obstacle in MapGenerator())
            {
                if (Map.Count < 15)
                    Map.AddLast(obstacle);
            }
        }


        private static void RemoveOldObjectsFromMap()
        {
            Map.RemoveAll(item =>
                item.Coordinates.Y < 0);

        }


    }

    static class MapExtensions
    {
        public static int RemoveAll<T>(this LinkedList<T> list, Predicate<T> match)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            var count = 0;
            var node = list.First;
            while (node != null)
            {
                var next = node.Next;
                if (match(node.Value))
                {
                    list.Remove(node);
                    count++;
                }
                node = next;
            }
            return count;
        }

        public static IObstacle FindMinElement(this LinkedList<IObstacle> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            var count = 0;
            var node = list.First;
            var min = double.MaxValue;
            IObstacle maxO = null;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value.Coordinates.Y < min)
                {
                    maxO = node.Value;
                    min = node.Value.Coordinates.Y;
                }
                node = next;
            }
            return maxO;
        }
    }
}
