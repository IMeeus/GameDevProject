using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.GameLogic.Input
{
    public interface IControls
    {
        Keys UpKey { get; }
        Keys DownKey { get; }
        Keys LeftKey { get; }
        Keys RightKey { get; }
        Keys ShootKey { get; }
    }

    /// <summary>
    /// Controls for an azerty keyboard.
    /// </summary>
    public class AzertyControls : IControls
    {
        public Keys UpKey => Keys.Z;

        public Keys DownKey => Keys.S;

        public Keys LeftKey => Keys.Q;

        public Keys RightKey => Keys.D;

        public Keys ShootKey => Keys.Space;
    }

    /// <summary>
    /// Controls for a qwerty keyboard.
    /// </summary>
    public class QwertyControls : IControls
    {
        public Keys UpKey => Keys.W;

        public Keys DownKey => Keys.S;

        public Keys LeftKey => Keys.A;

        public Keys RightKey => Keys.D;

        public Keys ShootKey => Keys.Space;
    }

    /// <summary>
    /// Controls with arrow keys.
    /// </summary>
    public class ArrowControls : IControls
    {
        public Keys UpKey => Keys.Up;

        public Keys DownKey => Keys.Down;

        public Keys LeftKey => Keys.Left;

        public Keys RightKey => Keys.Right;

        public Keys ShootKey => Keys.Space;
    }
}
