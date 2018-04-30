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
        public static List<IObstacle> Obstacles => Map.ToList();
        public static Player Player => GetPlayer();
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static IObstacle[,] Map;
        public static Dictionary<string, Action<IObstacle>> moves;
        private static double VerticalDistance = 15;
        private static int LevelHeight;
        private Func<int, IEnumerable<IObstacle>> MapGenerator;
        private static Player GetPlayer()
        {
            for (var x = 0; x < MapWidth; x++)
                for (var y = 0; y < MapHeight; y++)
                    if (Map[x, y] is Player)
                        return (Player)Map[x, y];
            throw new Exception("Player not found");
        }

        public Level(Func<int, IEnumerable<IObstacle>> mapGenerator, int mapHeight, int mapWidth)
        {
            MapGenerator = mapGenerator;
            Map = new IObstacle[mapWidth,mapHeight];
            for (var y = 0; y < mapHeight; y++)
                for (var x = 0; x < mapWidth; x++)
                    Map[x, y] = mapGenerator(y).First();

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
            Player.Coordinates = new Vector(currentCoordinates.X + distance * Math.Cos(angle),
                                (currentCoordinates.Y + VerticalDistance) % (MapHeight / 2));

        }

        public static void MoveObstacles()
        {
            foreach (var obstacle in Obstacles)
                if (!(obstacle is null))
                moves[obstacle.GetType().Name](obstacle);
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

            if (Player.Coordinates.Y <= 0 || Player.Health == 0)
                IsCompleted = true;
            RemoveOldObjectsFromMap();
            AddNewObjectsToMap();
        }

        private void AddNewObjectsToMap()
        {
            var isNullSegment = true;
            for (var y = 0; y < MapHeight; y++)
            {
                isNullSegment = true;
                var x = 0;
                for (; x < MapWidth; x++)
                {
                    if (Map[x, y] != null)
                        isNullSegment = false;
                }

                if (!isNullSegment) continue;

                foreach (var obstacle in MapGenerator(LevelHeight))
                    Map[x, y] = obstacle;
            }
                
        }

        private void RemoveOldObjectsFromMap()
        {
            for (var x = 0; x < MapWidth; x++)
            for (var y = 0; y < MapHeight; y++)
            {
                if (Map[x, y].Coordinates.Y - Player.Coordinates.Y <= 0)
                    Map[x, y] = null;
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
