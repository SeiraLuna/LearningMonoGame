using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningMonoGame
{
    public class SplashScreen : GameScreen
    {
        //Texture2D image;
        //public string Path;

        public Image Image;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            //image = content.Load<Texture2D>(Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                ScreenManager.Instance.ChangeScreens("SplashScreen");

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !ScreenManager.Instance.IsTransitioning)
            //    ScreenManager.Instance.ChangeScreens("SplashScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            //Image.Draw(spriteBatch, ScreenManager.Instance.Dimensions.X/2, ScreenManager.Instance.Dimensions.Y/2);
            //spriteBatch.Draw(image, destinationRectangle: new Rectangle((int)ScreenManager.Instance.Dimensions.X /2, (int)ScreenManager.Instance.Dimensions.Y / 2, image.Width, image.Height), origin: new Vector2(image.Width/2,image.Height/2));
            //spriteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}
