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

        private static Player GetPlayer()
        {
            for (var x = 0; x < MapWidth; x++)
                for (var y = 0; y < MapHeight; y++)
                    if (Map[x, y] is Player)
                        return (Player)Map[x, y];
            throw new Exception("Player not found");
        }

        public Level(IObstacle[,] map)
        {
            Map = map;
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
                                currentCoordinates.Y + distance * Math.Sin(angle));

        }

        public static void MoveObstacles()
        {
            foreach (var obstacle in Obstacles)
                moves[obstacle.GetType().Name](obstacle);
        }

        public void MoveGreenPlatform(GreenPlatform platform)
        {
           
        }

        public void MoveRedPlatform(RedPlatform platform)
        {

        }

        public void MoveUFO(UFO ufo)
        {

        }

        public void MoveBullet(Bullet bullet)
        {

        }

        public void UpdateMap()
        {
           //тут проверить объекты, пересекающиеся с плеером и обновить у них Health. Ну и у самого плеера тоже

            if (Player.Health == 0)
                IsCompleted = true;
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
