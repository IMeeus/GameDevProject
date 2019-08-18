using Astroids_Remake.Extra;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Tools
{
    public static class CollisionDetector
    {

        /// <summary>
        /// Checks if 2 circles collide.
        /// Reference: https://developer.mozilla.org/en-US/docs/Games/Techniques/2D_collision_detection
        /// </summary>
        /// <param name="source">The first circle that you wish to test.</param>
        /// <param name="target">The second circle that you wish to test.</param>
        /// <returns></returns>
        public static bool CheckCollision(Circle source, Circle target)
        {
            var xDistance = Math.Abs(source.Center.X - target.Center.X);
            var yDistance = Math.Abs(source.Center.Y - target.Center.Y);
            var distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

            return distance <= source.Radius + target.Radius;
        }

        /// <summary>
        /// Checks if a rectangle and a circle collide.
        /// Reference: https://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
        /// </summary>
        /// <param name="rect">The rectangle that you wish to test.</param>
        /// <param name="circle">The circle that you wish to test.</param>
        /// <returns></returns>
        public static bool CheckCollision(Rectangle rect, Circle circle)
        {
            var xDistance = Math.Abs(rect.Center.X - circle.Center.X);
            var yDistance = Math.Abs(rect.Center.Y - circle.Center.Y);

            if (xDistance > (rect.Width / 2) + circle.Radius) return false;
            if (yDistance > (rect.Height / 2) + circle.Radius) return false;

            if (xDistance <= rect.Width / 2) return true;
            if (yDistance <= rect.Height / 2) return true;

            double cornerDistance = Math.Pow(xDistance - (rect.Width / 2), 2) + Math.Pow(yDistance - (rect.Height / 2), 2);
            return cornerDistance <= Math.Pow(circle.Radius, 2);
        }
    }
}
