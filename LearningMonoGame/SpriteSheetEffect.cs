using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace LearningMonoGame
{
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;
        public Vector2 CurrentFrame;
        public Vector2 FrameCount;

        public int FrameWidth
        {
            get
            {
                if (_image.Texture() != null)
                    return _image.Texture().Width / (int)FrameCount.X;
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (_image.Texture() != null)
                    return _image.Texture().Height / (int)FrameCount.Y;
                return 0;
            }
        }

        public SpriteSheetEffect()
        {
            FrameCount = new Vector2(3, 4);
            CurrentFrame = new Vector2(1, 0);
            SwitchFrame = 100;
            FrameCounter = 0;
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
            if (_image.IsActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;

                    if (CurrentFrame.X * FrameWidth >= _image.Texture().Width)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = 1;

            _image.SourceRect = new Rectangle((int)CurrentFrame.X * FrameWidth, (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }
    }
}
