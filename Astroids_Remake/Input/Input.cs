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
        bool SpaceHold { get; }
        bool SpacePress { get; }
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
        public bool UpPress => _keyState.IsKeyDown(_controls.UpKey) && !_oldKeyState.IsKeyDown(_controls.UpKey);
        public bool DownHold => _keyState.IsKeyDown(_controls.DownKey);
        public bool DownPress => _keyState.IsKeyDown(_controls.DownKey) && !_oldKeyState.IsKeyDown(_controls.DownKey);
        public bool LeftHold => _keyState.IsKeyDown(_controls.LeftKey);
        public bool RightHold => _keyState.IsKeyDown(_controls.RightKey);
        public bool SpaceHold => _keyState.IsKeyDown(Keys.Space);
        public bool SpacePress => _keyState.IsKeyDown(Keys.Space) && !_oldKeyState.IsKeyDown(Keys.Space);
        public bool EnterHold => _keyState.IsKeyDown(Keys.Enter);
        public bool EnterPress => _keyState.IsKeyDown(Keys.Enter) && !_oldKeyState.IsKeyDown(Keys.Enter); 

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
