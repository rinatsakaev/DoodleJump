using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    interface IObstacle
    {
        Vector Coordinates { get; set; }
        int Health { get; set; }
        void Move(Vector toPoint);
        Image Image { get; set; }
        int Damage { get; set; }
         
    }
}
