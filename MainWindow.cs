using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1 {
    class MainWindow {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window

        private static RectangleF position = new RectangleF(100, 100, 20, 20);
        private static Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow };
        private static int currentColor = 0;

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            InputManager.Instance.Initialize(Window);

        }
        public static void Update(object sender, FrameEventArgs e) {
            InputManager.Instance.Update();
            if (InputManager.Instance.APressed(0)) {
                currentColor += 1;
                if (currentColor >= colors.Length) {
                    currentColor = 0;
                }
            }
            if (InputManager.Instance.BPressed(0)) {
                currentColor -= 1;
                if (currentColor <= 0) {
                    currentColor = colors.Length-1;
                }
            }
            if (InputManager.Instance.LeftStickX(0) < 0) { // < 0 = left
                position.X -= 80.0f * Math.Abs(InputManager.Instance.LeftStickX(0)) * (float)e.Time;
            }
            else if (InputManager.Instance.LeftStickX(0) > 0) { // < 0 = right
                position.X += 80.0f * Math.Abs(InputManager.Instance.LeftStickX(0)) * (float)e.Time;
            }

            if (InputManager.Instance.LeftStickY(0) < 0) { // y < 0 = down
                position.Y += 80.0f * Math.Abs(InputManager.Instance.LeftStickY(0)) * (float)e.Time;
            }
            else if (InputManager.Instance.LeftStickY(0) > 0) { // y > 0 = up
                position.Y -= 80.0f * Math.Abs(InputManager.Instance.LeftStickY(0)) * (float)e.Time;
            }

            InputManager.Instance.Update();
        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);
            if (!InputManager.Instance.IsConnected(0)) {
                GraphicsManager.Instance.DrawString("Please connect a controller", new PointF(10,10),Color.Red);
            }
            else if(!InputManager.Instance.HasAButton(0)){
                GraphicsManager.Instance.DrawString("A button not mapped, press A button", new PointF(10, 10), Color.Red);
                JoystickButton newAButton = JoystickButton.Button0;
                if (InputManager.Instance.GetButton(0, ref newAButton)) {
                    InputManager.Instance.GetMapping(0).A = newAButton;
                }
            }
            else if (!InputManager.Instance.HasBButton(0)) {
                GraphicsManager.Instance.DrawString("B button not mapped, press B button", new PointF(10, 10), Color.Red);
                JoystickButton newBButton = JoystickButton.Button1;
                if (InputManager.Instance.GetButton(0, ref newBButton,InputManager.Instance.GetMapping(0).A)) {
                    InputManager.Instance.GetMapping(0).B = newBButton;
                }
            }
            else if (!InputManager.Instance.HasDPad(0)) {
            }
            /*else if (!InputManager.Instance.HasLeftStick(0)) {
                InputManager.ControllerMapping map = InputManager.Instance.GetMapping(0);

                if (!map.HasLeftAxisX) {
                    GraphicsManager.Instance.DrawString("X axis is not found, move left stick horizontally", new PointF(10, 10), Color.Red);
                }
                else if (!map.HasLeftAxisY) {
                    GraphicsManager.Instance.DrawString("Y axis is not mouve, move left stick vertically", new PointF(10, 10), Color.Red);
                }
                JoystickAxis newAxis = JoystickAxis.Axis0;
                if (map.HasLeftAxisX) {
                    if (InputManager.Instance.GetAxis(0, ref newAxis, map.LeftAxisX)) {
                        map.LeftAxisY = newAxis;
                    }
                }
                else {
                    if (InputManager.Instance.GetAxis(0, ref newAxis)) {
                        map.LeftAxisX = newAxis;
                    }
                }
            }
             */
            else {
                GraphicsManager.Instance.DrawRect(position, colors[currentColor]);
            }
            int y = 0;
            JoystickState state = Joystick.GetState(0);
            InputManager.ControllerMapping buttons = InputManager.Instance.GetMapping(0);
            foreach (JoystickAxis enumVal in Enum.GetValues(typeof(JoystickAxis))) {
                Color clr = Color.Black;
                if (enumVal == buttons.LeftAxisX) {
                    clr = Color.Purple;
                }
                if (enumVal == buttons.LeftAxisY) {
                    clr = Color.Blue;
                }
                GraphicsManager.Instance.DrawString(enumVal.ToString() + ": " + state.GetAxis(enumVal), new PointF(10,30+y),clr);
                y += 20;
            }
            GraphicsManager.Instance.DrawRect(position, colors[currentColor]);

            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            InputManager.Instance.Shutdown();
            GraphicsManager.Instance.Shutdown();
        }
        [STAThread]
        public static void Main() {
            //create static(global) window instance
            Window = new OpenTK.GameWindow();

            //hook up the initialize callback
            Window.Load += new EventHandler<EventArgs>(Initialize);
            //hook up the update callback
            Window.UpdateFrame += new EventHandler<FrameEventArgs>(Update);
            //hook up render callback
            Window.RenderFrame += new EventHandler<FrameEventArgs>(Render);
            //hook up shutdown callback
            Window.Unload += new EventHandler<EventArgs>(Shutdown);
            Window.VSync = VSyncMode.On;

            //set window title and size
            Window.Title = "Game Name";
            Window.ClientSize = new Size(800, 600);
            //run game at 60fps. will not return until window is closed
            Window.Run(60.0f);
            
            Window.Dispose();
#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
