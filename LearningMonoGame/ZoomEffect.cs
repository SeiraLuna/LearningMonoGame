using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningMonoGame
{
    public class ZoomEffect : ImageEffect
    {
        public Vector2 Scale;

        public ZoomEffect()
        {
            Scale = new Vector2(1.5f, 1.5f);
            //_isTransitioning = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);    
        }

        public override void UnloadContent()
        {
            base.UnloadContent();   
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_image.IsActive && !ScreenManager.Instance.IsTransitioning)
                _image.Scale = Scale;
            else
                _image.Scale = Vector2.One;
        }
    }
}
