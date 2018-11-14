using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LearningMonoGame
{
    public class GameplayScreen :GameScreen
    {
        public Player player;
        private Map _map;

        public override void LoadContent()
        {
            base.LoadContent();

            var playerLoader = new SerializationManager<Player>();
            var mapLoader = new SerializationManager<Map>();
            player = playerLoader.Load("Load/Gameplay/Player.xml");
            _map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            player.LoadContent();
            _map.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            _map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            _map.Update(gameTime, ref player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            _map.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
