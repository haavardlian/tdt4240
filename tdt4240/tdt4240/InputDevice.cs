﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tdt4240
{
    class InputDevice : IDisposable
    {
        private InputType type;
        private PlayerIndex index;

        private static InputDevice keyboardInstance = null;
        private static int instances = 0;


        //TODO: This needs to be handled better.
        //Do we need the singleton or should we just not restrict the creation of keyboard inputs?
        public static InputDevice CreateInputDevice(InputType type, PlayerIndex index)
        {
            InputDevice device;
            if(type == InputType.Keyboard)
            {
                if (keyboardInstance == null)
                    keyboardInstance = new InputDevice(type, index);
                else if(index != keyboardInstance.Index)
                {
                    throw new Exception("Keyboard device alredy exists, and indexes do not match!");
                }

                device = keyboardInstance;
            }
            else
            {
                device = new InputDevice(type, index);
            }

            return device;
        }

        private InputDevice(InputType type, PlayerIndex index)
        {
            this.type = type;
            this.index = index;
            if (instances >= 4)
            {
                throw new Exception("Cannot have more than four input devices!");
            }
            instances++;
        }

        public PlayerIndex Index
        {
            get
            {
                return index;
            }
        }

        public bool IsConnected
        {
            get {
                if (type == InputType.Keyboard)
                    return true;
                else
                {
                    return GamePad.GetState(index).IsConnected;
                }
            }        
        }

        public Vector2 GetThumbstickVector()
        {
            if (this.type == InputType.Controller)
            {
                GamePadState state = GamePad.GetState(index);
                return state.ThumbSticks.Left;
            }
            else
            {
                Vector2 vec = Vector2.Zero;
                KeyboardState state = Keyboard.GetState(index);
                if(state.IsKeyDown(Keys.W))
                        vec.Y += 1;
                if(state.IsKeyDown(Keys.S))
                        vec.Y -= 1;
                if(state.IsKeyDown(Keys.A))
                        vec.X -= 1;
                if(state.IsKeyDown(Keys.D))
                        vec.X += 1;

                return vec;
            }
        }

        public bool IsButtonPressed(GameButtons button)
        {
            if (this.type == InputType.Controller)
            {
                GamePadState state = GamePad.GetState(index);
                switch(button)
                {
                    case GameButtons.Up:
                        return state.IsButtonDown(Buttons.DPadUp);
                    case GameButtons.Down:
                        return state.IsButtonDown(Buttons.DPadDown);
                    case GameButtons.Left:
                        return state.IsButtonDown(Buttons.DPadLeft);
                    case GameButtons.Right:
                        return state.IsButtonDown(Buttons.DPadRight);
                    case GameButtons.A:
                        return state.IsButtonDown(Buttons.A);
                    case GameButtons.B:
                        return state.IsButtonDown(Buttons.B);
                    case GameButtons.X:
                        return state.IsButtonDown(Buttons.X);
                    case GameButtons.Y:
                        return state.IsButtonDown(Buttons.Y);
                }
                
            }
            else
            {
                KeyboardState state = Keyboard.GetState(index);
                switch (button)
                {
                    case GameButtons.Up:
                        return state.IsKeyDown(Keys.W);
                    case GameButtons.Down:
                        return state.IsKeyDown(Keys.S);
                    case GameButtons.Left:
                        return state.IsKeyDown(Keys.A);
                    case GameButtons.Right:
                        return state.IsKeyDown(Keys.D);
                    case GameButtons.A:
                        return state.IsKeyDown(Keys.D1);
                    case GameButtons.B:
                        return state.IsKeyDown(Keys.D2);
                    case GameButtons.X:
                        return state.IsKeyDown(Keys.D3);
                    case GameButtons.Y:
                        return state.IsKeyDown(Keys.D4);
                }
            }
            return false;
        }

        public void Dispose()
        {
            if (instances > 0)
                instances--;
        }
    }

    class InputDeviceState
    {
        GameButtons[] Buttons;
    }


    public enum InputType
    {
        Controller,
        Keyboard
    }

    public enum GameButtons
    {
        A,
        B,
        X,
        Y,
        Up,
        Down,
        Left,
        Right
    }
}
