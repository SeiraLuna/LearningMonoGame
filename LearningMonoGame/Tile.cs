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
        private string _state;

        public Rectangle SourceRect
        {
            get { return _sourceRect; }
        }

        public Vector2 Position
        {
            get { return _postion; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this._postion = position;
            this._sourceRect = sourceRect;
            this._state = state;
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            if(_state == "Solid")
            {
                var tileRect = new Rectangle((int)Position.X, (int)Position.Y, _sourceRect.Width, _sourceRect.Height);
                var playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);

                if (playerRect.Intersects(tileRect))
                {
                    if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;
                    else if (player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;
                    else if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;
                    else if (player.Velocity.Y > 0)
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
