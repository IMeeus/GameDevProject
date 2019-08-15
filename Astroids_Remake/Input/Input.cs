using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.GameLogic.Input
{
    public interface IInput
    {
        bool UpHold { get; }
        bool DownHold { get; }
        bool LeftHold { get; }
        bool RightHold { get; }
        bool ShootHold { get; }
        bool ShootPress { get; }
        void Update();
        void SetControls(IControls newControls);
    }

    /// <summary>
    /// An input 'interface' that holds all the possible input events.
    /// </summary>
    public class Input : IInput
    {
        private KeyboardState _keyState, _oldKeyState;
        private IControls _controls;

        public Input(IControls initialControls)
        {
            _controls = initialControls;
        }

        public bool UpHold => _keyState.IsKeyDown(_controls.UpKey);
        public bool DownHold => _keyState.IsKeyDown(_controls.DownKey);
        public bool LeftHold => _keyState.IsKeyDown(_controls.LeftKey);
        public bool RightHold => _keyState.IsKeyDown(_controls.RightKey);
        public bool ShootHold => _keyState.IsKeyDown(_controls.ShootKey);
        public bool ShootPress => _keyState.IsKeyDown(_controls.ShootKey) && !_oldKeyState.IsKeyDown(_controls.ShootKey);

        /// <summary>
        /// Updates the old and new keyboard state.
        /// </summary>
        public void Update()
        {
            _oldKeyState = _keyState;
            _keyState = Keyboard.GetState();
        }

        /// <summary>
        /// Changes the keys that the input events react to.
        /// </summary>
        /// <param name="newControls">The new input controls</param>
        public void SetControls(IControls newControls)
        {
            _controls = newControls;
        }
    }
}
