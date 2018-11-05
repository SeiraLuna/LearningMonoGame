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
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public bool IsActive;
      
        private Texture2D _texture;
        private Vector2 _origin;
        private ContentManager _content;
        private RenderTarget2D _renderTarget;
        private SpriteFont _font;
        private Dictionary<string, ImageEffect> _effectList;
        public string Effects;

        public FadeEffect FadeEffect;

        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            _effectList.Add(effect.GetType().ToString().Replace("LearningMonoGame.", ""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (_effectList.ContainsKey(effect))
            {
                _effectList[effect].IsActive = true;
                var obj = this;
                _effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (_effectList.ContainsKey(effect))
            {
                _effectList[effect].IsActive = false;
                _effectList[effect].UnloadContent();
            }
        }

        public Image()
        {
            Path = Text = Effects = String.Empty;
            FontName = "Fonts/Calisto MT";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            _effectList = new Dictionary<string, ImageEffect> ();
        }

        public void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                _texture = _content.Load<Texture2D>(Path);

            _font = _content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (_texture != null)
                dimensions.X += Math.Max(_texture.Width, _font.MeasureString(Text).X);
            else
                dimensions.X += _font.MeasureString(Text).X;
            
            if(_texture != null) 
                dimensions.Y += Math.Max(_texture.Height, _font.MeasureString(Text).Y);
            else
                dimensions.Y += _font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X , (int)dimensions.Y ) ;

            var offset = new Vector2((dimensions.X * Scale.X) / 2, (dimensions.Y * Scale.Y) / 2);

            _renderTarget = new RenderTarget2D(ScreenManager.Instance.graphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.graphicsDevice.SetRenderTarget(_renderTarget);
            ScreenManager.Instance.graphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.spriteBatch.Begin();

            //new Vector2(_font.MeasureString(Text).X/2 - _texture.Width/2

            if (_texture != null)
                ScreenManager.Instance.spriteBatch.Draw(_texture, new Vector2(dimensions.X/2 - _texture.Width/2,0), Color.White);
            ScreenManager.Instance.spriteBatch.DrawString(_font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.spriteBatch.End();

            _texture = _renderTarget;

            ScreenManager.Instance.graphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);

            if(Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string s in split)
                    ActivateEffect(s);
            }
 
        }

        public void UnloadContent()
        {
            _content.Unload();
            foreach (var effect in _effectList)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in _effectList)
            {
                if(effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(_texture, Position , SourceRect, Color.White * Alpha, 0.0f, _origin, Scale, SpriteEffects.None, 0.0f);
        }

        public void Draw(SpriteBatch spriteBatch, float xPos, float yPos)
        {
            Position.X = xPos;
            Position.Y = yPos;
            _origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(_texture, Position, SourceRect, Color.White * Alpha, 0.0f, _origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
