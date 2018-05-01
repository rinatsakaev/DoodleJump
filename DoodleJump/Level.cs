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
        public static int LevelHeight { get; private set; }
        private static int ScreenHeight;
        private Func<IEnumerable<IObstacle>> MapGenerator;
        private int AverageObjectHeight = 10;
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
            Map.Enqueue(new Player(new Vector(10, 10)));
            AddNewObjectsToMap();
            InitializeMoves();
        }

        private void InitializeMoves()
        {
            moves["GreenPlatform"] = x => MoveGreenPlatform((GreenPlatform)x);
            moves["RedPlatform"] = x => MoveRedPlatform((RedPlatform)x);
            moves["UFO"] = x => MoveUFO((UFO)x);
            moves["Bullet"] = x => MoveBullet((Bullet)x);
        }

        public static void MovePlayer(double angle, double distance)
        {
            var currentCoordinates = Player.Coordinates;
            var toPoint = new Vector(currentCoordinates.X + distance * Math.Cos(angle),
                currentCoordinates.Y + VerticalDistance);
            Player.Move(toPoint);

        }

        public static void MoveObstacles()
        {
            var currentElement = Map.Head;
            for (var i = 0; i < Map.Count; i++)
            {
                if (currentElement.Value is Player)
                    continue;
                currentElement = currentElement.Next;
                moves[currentElement.Value.GetType().Name](currentElement.Value);
            }

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

        }


        public void UpdateMap()
        {

            //тут проверить объекты, пересекающиеся с плеером и обновить у них Health. Ну и у самого плеера тоже
            var currentElement = Map.Head;
            for (var i = 0; i < Map.Count; i++)
            {
                if (currentElement.Value.Coordinates == Player.Coordinates)
                {
                    if (currentElement.Value is UFO)
                        Player.Health--;
                    if (currentElement.Value is RedPlatform)
                        currentElement.Value.Health--;
                }
                currentElement = currentElement.Next;
            }

            if (Player.Health == 0)
                IsCompleted = true;
            RemoveOldObjectsFromMap();
            AddNewObjectsToMap();
        }

        private void AddNewObjectsToMap()
        {
            foreach (var obstacle in MapGenerator().Take(ScreenHeight / AverageObjectHeight))
                Map.Enqueue(obstacle);
            LevelHeight += ScreenHeight / AverageObjectHeight;
        }


        private void RemoveOldObjectsFromMap()
        {
            var currentElement = Map.Tail;
            while (currentElement.Value.Coordinates.Y - LevelHeight <= 0)
            {
                if (Map.DequeueFromTail() is Player)
                    IsCompleted = true;
                currentElement = Map.Tail;
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
