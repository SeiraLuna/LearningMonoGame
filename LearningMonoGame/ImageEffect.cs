using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LearningMonoGame
{
    public class ImageEffect
    {
        protected Image _image;
        public bool IsActive;

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this._image = image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update (GameTime gameTime)
        {
        }
    }
}
