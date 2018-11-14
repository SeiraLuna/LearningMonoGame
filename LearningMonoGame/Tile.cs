using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningMonoGame
{
    public class Tile
    {
        private Vector2 _postion;
        private Rectangle _sourceRect;

        public Rectangle SourceRect
        {
            get { return _sourceRect; }
        }

        public Vector2 Position
        {
            get { return _postion; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect)
        {
            this._postion = position;
            this._sourceRect = sourceRect;
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
