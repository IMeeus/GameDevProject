using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Extra
{
    public interface IAnimation
    {
        bool Playing { get; }
        Texture2D Texture { get; }
        Rectangle CurrentFrame { get; }
        List<Rectangle> Frames { get; }

        void AddFrame(Rectangle rectangle);
        void Update(float deltaTime);
        void Start();
        void Stop();
    }

    /// <summary>
    /// Describes the animation of a sprite.
    /// The texture contains the spritesheet.
    /// The frames describe the areas that have to be drawn.
    /// </summary>
    public class Animation : IAnimation
    {
        private int _currentFrameIndex;
        private float _time;
        private float _framesPerSecond;
        private bool _repeat;

        public bool Playing { get; private set; }
        public Texture2D Texture { get; private set; }
        public Rectangle CurrentFrame => Frames[_currentFrameIndex];
        public List<Rectangle> Frames { get; private set; }

        public Animation(Texture2D texture, float framesPerSecond, bool repeat)
        {
            _currentFrameIndex = 0;
            _time = 0;
            _framesPerSecond = framesPerSecond;
            _repeat = repeat;

            Playing = false;
            Texture = texture;
            Frames = new List<Rectangle>();
        }

        /// <summary>
        /// Adds a frame to the animation
        /// </summary>
        /// <param name="rectangle">The area that has to be displayed of the spritesheet.</param>
        public void AddFrame(Rectangle rectangle)
        {
            Frames.Add(rectangle);
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            if (!Playing) return;

            _time += deltaTime;
            SwitchFrames();
            CheckCompletion();
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Start()
        {
            _time = 0;
            _currentFrameIndex = 0;
            Playing = true;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            _time = 0;
            _currentFrameIndex = 0;
            Playing = false;
        }


        /// <summary>
        /// Changes the currentFrame depending on the frames per second.
        /// </summary>
        private void SwitchFrames()
        {
            if (_time >= 1 / _framesPerSecond)
            {
                _time = 0;
                _currentFrameIndex += 1;
            }
        }

        /// <summary>
        /// Checks for completion of the animation.
        /// </summary>
        private void CheckCompletion()
        {
            if (_currentFrameIndex >= Frames.Count)
            {
                _currentFrameIndex = 0;

                if (!_repeat)
                    Stop();
            }
        }

        
    }
}
