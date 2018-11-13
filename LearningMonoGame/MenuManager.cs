using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningMonoGame
{
    public class MenuManager
    {
        private Menu _menu;
        private bool _isTransitioning;

        private void Transition(GameTime gameTime)
        {
            if (_isTransitioning)
            {
                for(int i = 0; i < _menu.Items.Count; i++)
                {
                    _menu.Items[i].Image.Update(gameTime);
                    float first = _menu.Items[0].Image.Alpha;
                    float last = _menu.Items[_menu.Items.Count - 1].Image.Alpha;
                    if (first == 0 && last == 0)
                        _menu.ID = _menu.Items[_menu.ItemNumber].LinkID;
                    else if (first == 1.0f && last == 1.0f)
                    {
                        _isTransitioning = false;
                        foreach(MenuItem item in _menu.Items)
                        {
                            item.Image.RestroreEffects();
                        }
                    }
                }
            }
        }

        public MenuManager()
        {
            _menu = new Menu();
            _menu.OnMenuChange += _menu_OnMenuChange;
            //foreach(MenuItem item in _menu.Items)
            //{
            //    foreach(ImageEffect effect in item.Image.EffectList.Values)
            //    {
            //        _menu.OnMenuChange += effect.Effect_OnMenuChange;
            //    }
            //}
        }

        private void _menu_OnMenuChange(object sender, EventArgs e)
        {
            var serializationManager = new SerializationManager<Menu>();
            _menu.UnloadContent();            
            _menu = serializationManager.Load(_menu.ID);
            _menu.LoadContent();
            _menu.OnMenuChange += _menu_OnMenuChange;
            _menu.Transition(0.0f);

            foreach (MenuItem item in _menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("FadeEffect");
            }
        }

        public void LoadContent(string menuPath)
        {
            if(menuPath != String.Empty)
                _menu.ID = menuPath;            
        }

        public void UnloadContent()
        {
            _menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if(!_isTransitioning)
                _menu.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Enter) && !_isTransitioning)
            {
                if (_menu.Items[_menu.ItemNumber].LinkType == "Screen")
                    ScreenManager.Instance.ChangeScreens(_menu.Items[_menu.ItemNumber].LinkID);
                else
                {
                    _isTransitioning = true;
                    _menu.Transition(1.0f);
                    foreach(MenuItem item in _menu.Items)
                    {
                        item.Image.StoreEffects();
                        item.Image.ActivateEffect("FadeEffect");
                    }
                }
            }
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _menu.Draw(spriteBatch);
        }

    }
}
