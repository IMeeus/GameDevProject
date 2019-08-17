using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Extra
{
    public class Circle
    {
        public float Radius { get; set; }
        public float Diameter => Radius * 2;
        public float Right => Position.X + Radius;
        public float Left => Position.X - Radius;
        public float Top => Position.Y - Radius;
        public float Bottom => Position.Y + Radius;
        public Vector2 Position { get; set; }

        public Circle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }
    }
}
