using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LearningMonoGame
{
    public class ScreenManager
    {
        private static ScreenManager _instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }
        SerilizationManager<GameScreen> SerializationGameScreenManager;

        GameScreen currentScreen;

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScreenManager();

                return _instance;
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);            
            currentScreen = new SplashScreen();
            SerializationGameScreenManager = new SerilizationManager<GameScreen>();
            SerializationGameScreenManager.Type = currentScreen.Type;
            currentScreen = SerializationGameScreenManager.Load("./Load/SplashScreen.xml");
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }


    }
}
