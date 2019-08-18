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
        public float Right => Center.X + Radius;
        public float Left => Center.X - Radius;
        public float Top => Center.Y - Radius;
        public float Bottom => Center.Y + Radius;
        public Vector2 Center { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }
    }
}
