using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace LearningMonoGame
{
    public class ScreenManager
    {
        private static ScreenManager _instance;
        public Vector2 Dimensions;
        //{ private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        SerializationManager<GameScreen> SerializationGameScreenManager;

        private GameScreen _currentScreen, _newScreen;
        [XmlIgnore]
        public GraphicsDevice graphicsDevice;
        [XmlIgnore]
        public SpriteBatch spriteBatch;

        public Image Image;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var serilizationManager = new SerializationManager<ScreenManager>();
                    _instance = serilizationManager.Load("Load/ScreenManager.xml");
                    //_instance = new ScreenManager();
                }
                return _instance;
            }
        }

        public void ChangeScreens(string screenName)
        {
            _newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("LearningMonoGame." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        private void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    _currentScreen.UnloadContent();
                    _currentScreen = _newScreen;
                    SerializationGameScreenManager.Type = _currentScreen.Type;
                    if (File.Exists(_currentScreen.xmlPath))
                        _currentScreen = SerializationGameScreenManager.Load(_currentScreen.xmlPath);
                    _currentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(640, 480);
            //_currentScreen = new GameplayScreen();
            _currentScreen = new SplashScreen();
            SerializationGameScreenManager = new SerializationManager<GameScreen>();
            SerializationGameScreenManager.Type = _currentScreen.Type;
            _currentScreen = SerializationGameScreenManager.Load("./Load/SplashScreen.xml");

            //Dimensions = new Vector2(640, 480);            
            //_currentScreen = new SplashScreen();
            //SerializationGameScreenManager = new SerializationManager<GameScreen>();
            //SerializationGameScreenManager.Type = _currentScreen.Type;
            //_currentScreen = SerializationGameScreenManager.Load("./Load/SplashScreen.xml");
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            _currentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            _currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }


    }
}
