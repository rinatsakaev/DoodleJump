using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    class Level
    {
        public static bool IsCompleted;
        public static Player Player => GetPlayer();
        public static Queue<IObstacle> Map;
        public static Dictionary<string, Action<IObstacle>> moves;
        private static double VerticalDistance = 15;
        private static int ScreenHeight;
        private static Func<IEnumerable<IObstacle>> MapGenerator;
        private static Player GetPlayer()
        {
            var currentElement = Map.Head;
            for (var i = 0; i < Map.Count; i++)
            {
                if (currentElement.Value is Player)
                    return currentElement.Value as Player;
                currentElement = currentElement.Next;
            }

            throw new Exception("Player not found");
        }

        public Level(Func<IEnumerable<IObstacle>> mapGenerator, int screenHeight)
        {
            ScreenHeight = screenHeight;
            MapGenerator = mapGenerator;
            Map = new Queue<IObstacle>();
            Map.Enqueue(new Player(new Vector(250, 400)));
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
            var currentCoordinates = Player.Coordinates;
            var toPoint = new Vector(currentCoordinates.X + distance * Math.Cos(angle),
                currentCoordinates.Y + VerticalDistance);
            Player.Move(toPoint);
        }

        public static void MoveObjects(double playerAngle, double distance)
        {
            MovePlayer(playerAngle, distance);
            var currentElement = Map.Head;
            for (var i = 0; i < Map.Count; i++)
            {
                if (currentElement.Value is Player)
                    continue;
                currentElement = currentElement.Next;
                moves[currentElement.Value.GetType().Name](currentElement.Value);
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
            platform.Move(new Vector(platform.Coordinates.X + 1, platform.Coordinates.Y));
        }


        public static void UpdateMap()
        {
            var currentElement = Map.Head;
            for (var i = 0; i < Map.Count; i++)
            {
                if (!(currentElement.Value is Player) && currentElement.Value.Coordinates == Player.Coordinates)
                {
                    Player.Health -= currentElement.Value.Damage;
                    if (currentElement.Value is RedPlatform)
                        currentElement.Value.Health -= Player.Damage;
                }
                else
                {
                    Player.Move(new Vector(Player.Coordinates.X,
                        Player.Coordinates.Y - VerticalDistance));
                }
                currentElement = currentElement.Next;
            }

            if (Player.Health == 0)
                IsCompleted = true;
            RemoveOldObjectsFromMap();
            AddNewObjectsToMap();
        }

        private static void AddNewObjectsToMap()
        {
            foreach (var obstacle in MapGenerator())
            {
                if (!(Map.Count < 8 && Player.Coordinates.Y % ScreenHeight >= ScreenHeight / 2))
                    break;
                Map.Enqueue(obstacle);
            }
 

        }


        private static void RemoveOldObjectsFromMap()
        {
            var currentElement = Map.Tail;
            for (var i = 0; i < Map.Count; i++)
            {
                if (!(currentElement.Value.Coordinates.Y - Player.Coordinates.Y > ScreenHeight / 2))
                    continue;
                if (currentElement.Value is Player)
                    IsCompleted = true;
               
            }
        }


    }

    static class MapExtensions
    {
        public static List<T> ToList<T>(this T[,] map)
        {
            var mapList = new List<T>();
            for (var x = 0; x < map.GetLength(0); x++)
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    mapList.Add(map[x, y]);
                }

            return mapList;
        }
    }
}
