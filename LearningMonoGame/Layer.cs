using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningMonoGame
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Rows;

            public TileMap()
            {
                Rows = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tiles;
        public Image DrawTile;
        private List<Tile> _tiles;

        public Layer()
        {
            DrawTile = new Image();
            _tiles = new List<Tile>();
        }

        public void LoadContent(Vector2 tileDimensions)
        {
            DrawTile.LoadContent();
            Vector2 position = -tileDimensions;

            foreach(string row in Tiles.Rows)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;

                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        _tiles.Add(new Tile());
 
                        string str = s.Replace("[", String.Empty);
                        int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                        _tiles[_tiles.Count - 1].LoadContent(position, new Rectangle(value1*(int)tileDimensions.X, value2 * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y));
                    }
                }
            }
        }

        public void UnloadContent()
        {
            DrawTile.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            DrawTile.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in _tiles)
            {
                DrawTile.Position = tile.Position;
                DrawTile.SourceRect = tile.SourceRect;
                DrawTile.Draw(spriteBatch);
            }
        }
    }
}
