using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Extra
{
    /// <summary>
    /// Enum of layer depths.
    /// Made use of a struct to make the enum return float values to be compatible with the layerDepth parameter of the SpriteBatch.Draw method.
    /// </summary>
    public struct LayerDepth
    {
        public const float BACKGROUND = 0f;
        public const float MAIN = .1f;
    }
}