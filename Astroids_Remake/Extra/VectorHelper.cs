using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Extra
{
    /// <summary>
    /// A tool that helps with vector operations.
    /// </summary>
    public static class VectorHelper
    {
        /// <summary>
        /// Converts an angle into a vector.
        /// </summary>
        /// <param name="angle">The angle that you wish to convert.</param>
        /// <returns>The converted angle.</returns>
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        /// <summary>
        /// Converts a vector into an angle.
        /// </summary>
        /// <param name="vector">The vector that you wish to convert.</param>
        /// <returns>The converted vector.</returns>
        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }
    }
}
