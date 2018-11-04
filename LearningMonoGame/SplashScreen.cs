using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningMonoGame
{
    public class SplashScreen : GameScreen
    {
        Texture2D image;
        public string Path;

        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, destinationRectangle: new Rectangle((int)ScreenManager.Instance.Dimensions.X /2, (int)ScreenManager.Instance.Dimensions.Y / 2, image.Width, image.Height), origin: new Vector2(image.Width/2,image.Height/2));
            //spriteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}
