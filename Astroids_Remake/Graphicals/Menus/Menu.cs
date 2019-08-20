using Astroids_Remake.Extra;
using Astroids_Remake.GameLogic.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astroids_Remake.Graphicals.Menus
{
    public abstract class Menu
    {
        private readonly Input _input;

        private int _selectedItemIndex;
        private int _marginBetweenItems;
        private float _textScale;
        private Dictionary<string, string> _items;
        private Vector2 _position;
        private SpriteFont _font;

        private float _itemHeight => _font.MeasureString("Hello World!").Y;
        private Vector2 _dimensions => CalculateDimensions();
        private Vector2 _origin => new Vector2(_dimensions.X / 2, _dimensions.Y / 2);
        
        protected string SelectedItem => _items.ElementAt(_selectedItemIndex).Key;

        public Menu(Vector2 position, SpriteFont font, float textScale, int marginBetweenItems, Input input)
        {
            _input = input;
            _selectedItemIndex = 0;
            _marginBetweenItems = marginBetweenItems;
            _textScale = textScale;
            _items = new Dictionary<string, string>();
            _position = position;
            _font = font;

            LoadItems();
        }

        protected abstract void LoadItems();
        protected abstract void SelectItem();

        /// <summary>
        /// Adds an item to the menu.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The value of the item.</param>
        protected void AddItem(string key, string value)
        {
            _items.Add(key, value);
        }

        /// <summary>
        /// Calculates the dimensions of the menu.
        /// </summary>
        /// <returns>Returns the dimensions of the menu.</returns>
        private Vector2 CalculateDimensions()
        {
            List<float> allWidths = new List<float>();
            List<float> allHeights = new List<float>();

            foreach (string item in _items.Values)
            {
                Vector2 itemDimensions = _font.MeasureString(item);
                allWidths.Add(itemDimensions.X);
                allHeights.Add(itemDimensions.Y);
            }

            return new Vector2(allWidths.Max(), allHeights.Sum() + _marginBetweenItems * (_items.Count - 1));
        }

        /// <summary>
        /// Updates the menu.
        /// </summary>
        /// <param name="deltaTime">The deltatime of the last gamecycle.</param>
        public void Update(float deltaTime)
        {
            HandleInput();
        }

        /// <summary>
        /// Handles the input of the menu.
        /// </summary>
        private void HandleInput()
        {
            if (_input.UpPress)
                PreviousItem();
            if (_input.DownPress)
                NextItem();
            if (_input.EnterPress)
                SelectItem();
        }

        /// <summary>
        /// Select the next item.
        /// </summary>
        private void NextItem()
        {
            _selectedItemIndex++;

            if (_selectedItemIndex >= _items.Count)
                _selectedItemIndex = 0;
        }

        /// <summary>
        /// Select the previous item.
        /// </summary>
        private void PreviousItem()
        {
            _selectedItemIndex--;

            if (_selectedItemIndex < 0)
                _selectedItemIndex = _items.Count - 1;
        }

        /// <summary>
        /// Draws the menu.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that is used to draw on the screen.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                string text = _items.ElementAt(i).Value;
                Vector2 itemDimensions = _font.MeasureString(text);
                float xPosition = _position.X;
                float yPosition = _position.Y - _dimensions.Y / 2 + ((_itemHeight * _textScale + _marginBetweenItems) * i) + _itemHeight / 2;
                Vector2 position = new Vector2(xPosition, yPosition);
                Color color = i == _selectedItemIndex ? Color.Red : Color.White;
                Vector2 origin = new Vector2(itemDimensions.X / 2, itemDimensions.Y / 2);

                spriteBatch.DrawString(_font, text, position, color, 0f, origin, _textScale, SpriteEffects.None, LayerDepth.MAIN);
            }
        }
    }
}
