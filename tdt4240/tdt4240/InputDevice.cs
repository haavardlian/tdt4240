using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace tdt4240
{
    public class InputDevice : IDisposable
    {
        private InputType type;
        private PlayerIndex index;
        private static InputState inputState;

        private static InputDevice keyboardInstance = null;
        private static int instances = 0;


        public static InputState InputState
        {
            get
            {
                if(inputState == null)
                    inputState = new InputState();
                return inputState;
            }
        }

        public InputType Type
        {
            get
            {
                return type;
            }
        }

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

            if(inputState == null)
                inputState = new InputState();

            if(type == InputType.Keyboard)
                this.index = PlayerIndex.One;
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
                Vector2 vec = state.ThumbSticks.Left;
                vec.Y *= -1;
                return vec;
            }
            else
            {
                Vector2 vec = Vector2.Zero;
                KeyboardState state = Keyboard.GetState(index);
                if(state.IsKeyDown(Keys.W))
                        vec.Y -= 1;
                if(state.IsKeyDown(Keys.S))
                        vec.Y += 1;
                if(state.IsKeyDown(Keys.A))
                        vec.X -= 1;
                if(state.IsKeyDown(Keys.D))
                        vec.X += 1;

                if (vec.Length() > 1)
                {
                    vec *= 1 / vec.Length();
                }

                return vec;
            }
        }

        public bool IsButtonPressed(GameButtons button)
        {
            if (this.type == InputType.Controller)
            {
                GamePadState state = GamePad.GetState(index);
                PlayerIndex temp;
                switch(button)
                {
                    case GameButtons.Up:
                        return inputState.IsNewButtonPress(Buttons.DPadUp, index, out temp);
                    case GameButtons.Down:
                        return inputState.IsNewButtonPress(Buttons.DPadDown, index, out temp);
                    case GameButtons.Left:
                        return inputState.IsNewButtonPress(Buttons.DPadLeft, index, out temp);
                    case GameButtons.Right:
                        return inputState.IsNewButtonPress(Buttons.DPadRight, index, out temp);
                    case GameButtons.A:
                        return inputState.IsNewButtonPress(Buttons.A, index, out temp);
                    case GameButtons.B:
                        return inputState.IsNewButtonPress(Buttons.B, index, out temp);
                    case GameButtons.X:
                        return inputState.IsNewButtonPress(Buttons.X, index, out temp);
                    case GameButtons.Y:
                        return inputState.IsNewButtonPress(Buttons.Y, index, out temp);
                    case GameButtons.Start:
                        return inputState.IsNewButtonPress(Buttons.Start, index, out temp);
                    case GameButtons.Back:
                        return inputState.IsNewButtonPress(Buttons.Back, index, out temp);
                }
                
            }
            else
            {
                KeyboardState state = Keyboard.GetState(index);
                PlayerIndex temp;
                switch (button)
                {
                    case GameButtons.Up:
                        return inputState.IsNewKeyPress(Keys.W, index, out temp) ||
                               inputState.IsNewKeyPress(Keys.Up, index, out temp);
                    case GameButtons.Down:
                        return inputState.IsNewKeyPress(Keys.S, index, out temp) ||
                               inputState.IsNewKeyPress(Keys.Down, index, out temp);
                    case GameButtons.Left:
                        return inputState.IsNewKeyPress(Keys.A, index, out temp) ||
                               inputState.IsNewKeyPress(Keys.Left, index, out temp);
                    case GameButtons.Right:
                        return inputState.IsNewKeyPress(Keys.D, index, out temp) ||
                               inputState.IsNewKeyPress(Keys.Right, index, out temp);
                    case GameButtons.A:
                        return inputState.IsNewKeyPress(Keys.D1, index, out temp);
                    case GameButtons.B:
                        return inputState.IsNewKeyPress(Keys.D2, index, out temp);
                    case GameButtons.X:
                        return inputState.IsNewKeyPress(Keys.D3, index, out temp);
                    case GameButtons.Y:
                        return inputState.IsNewKeyPress(Keys.D4, index, out temp);
                    case GameButtons.Start:
                        return inputState.IsNewKeyPress(Keys.Enter, index, out temp);
                    case GameButtons.Back:
                        return inputState.IsNewKeyPress(Keys.Back, index, out temp);
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
        Right,
        Start,
        Back
    }
}
