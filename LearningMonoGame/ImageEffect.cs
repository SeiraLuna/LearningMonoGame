using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace LearningMonoGame
{
    public class ImageEffect
    {
        protected Image _image;
        public bool IsActive;
        //[XmlIgnore]
        //protected bool _isTransitioning;

        //public void Effect_OnMenuChange(object sender, EventArgs e)
        //{
        //    _isTransitioning = true;
        //}

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
