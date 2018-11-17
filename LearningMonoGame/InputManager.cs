using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningMonoGame
{
    public class InputManager
    {
        private KeyboardState _currentKeyState, _prevKeyState;

            private static InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                return _instance == null ? _instance = new InputManager() : _instance;
            }
        }

        public void Update()
        {
           
            _prevKeyState = _currentKeyState;
            _currentKeyState = !ScreenManager.Instance.IsTransitioning ? Keyboard.GetState() : _currentKeyState;
           // if (!ScreenManager.Instance.IsTransitioning)
             //   _currentKeyState = Keyboard.GetState();
                      
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach(Keys k in keys)
            {
                if (_currentKeyState.IsKeyDown(k) && _prevKeyState.IsKeyUp(k))
                    return true;
            }
            return false;
        }

        
        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys k in keys)
            {
                if (_currentKeyState.IsKeyUp(k) && _prevKeyState.IsKeyDown(k))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys k in keys)
            {
                if (_currentKeyState.IsKeyDown(k))
                    return true;
            }
            return false;
        }
    }
}
