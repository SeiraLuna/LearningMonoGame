using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningMonoGame
{
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;
        private int _itemNumber;
        private string _id;

        public int ItemNumber
        {
            get { return _itemNumber; }
        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnMenuChange(this, null);
            }
        }

        public void Transition (float alpha)
        {
            foreach(MenuItem m in Items)
            {
                m.Image.IsActive = true;
                m.Image.Alpha = alpha;
                if (alpha == 0.0f)
                    m.Image.FadeEffect.Increase = true;
                else
                    m.Image.FadeEffect.Increase = false;
            }
        }

        private void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (MenuItem m in Items)
                dimensions += new Vector2(m.Image.SourceRect.Width, m.Image.SourceRect.Height);

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) /2, (ScreenManager.Instance.Dimensions.Y - dimensions.Y) /2);

            foreach(MenuItem m in Items)
            {
                if(Axis == "X")
                {
                    m.Image.Position = new Vector2(dimensions.X, (ScreenManager.Instance.Dimensions.Y - m.Image.SourceRect.Height) / 2);
                }
                else if (Axis == "Y")
                    m.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X - m.Image.SourceRect.Width)/2, dimensions.Y);

                dimensions += new Vector2(m.Image.SourceRect.Width, m.Image.SourceRect.Height);
            }
        }

        public Menu()
        {
            _id = String.Empty;
            _itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach(MenuItem m in Items)
            {
                m.Image.LoadContent();
                foreach (string s in split)
                    m.Image.ActivateEffect(s);
            }
            AlignMenuItems();
        }

        public void UnloadContent()
        {
            foreach (MenuItem m in Items)
                m.Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    _itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    _itemNumber--;
            }

            if(Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    _itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    _itemNumber--;
            }

            if (_itemNumber < 0)
                _itemNumber = 0;
            else if (_itemNumber > Items.Count - 1)
                _itemNumber = Items.Count - 1;

            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].Image.IsActive = i == _itemNumber ? true : false;

                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem m in Items)
                m.Image.Draw(spriteBatch);
        }
    }
}
