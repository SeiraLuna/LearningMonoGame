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
        public string SolidTiles, OverlayTiles;
        private List<Tile> _underlayTiles, _overlayTiles;
        private string _state;

        public Layer()
        {
            DrawTile = new Image();
            _underlayTiles = new List<Tile>();
            _overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = String.Empty;
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
                        if (!s.Contains('x'))
                        {
                            _state = "Passive";
                            var tile = new Tile();
                            //_udnerlayTiles.Add(new Tile());

                            string str = s.Replace("[", String.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                _state = "Solid";

                            tile.LoadContent(position, new Rectangle(value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y), _state);
                            //_udnerlayTiles[_udnerlayTiles.Count - 1].LoadContent(position, new Rectangle(value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y), _state);

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                _overlayTiles.Add(tile);
                            else
                                _underlayTiles.Add(tile);

                        }

                    }
                }
            }
        }

        public void UnloadContent()
        {
            DrawTile.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach(Tile tile in _underlayTiles)
                tile.Update(gameTime, ref player);
            foreach (Tile tile in _overlayTiles)
                tile.Update(gameTime, ref player);

            //foreach (Tile tile in _udnerlayTiles)
            //    tile.Update(gameTime, ref player);

        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            List<Tile> tiles;
            if (drawType == "Underlay")
                tiles = _underlayTiles;
            else
                tiles = _overlayTiles;


            foreach(Tile tile in tiles)
            {
                DrawTile.Position = tile.Position;
                DrawTile.SourceRect = tile.SourceRect;
                DrawTile.Draw(spriteBatch);
            }
        }
    }
}
