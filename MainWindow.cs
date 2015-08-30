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
        
        static List<OpenTK.Input.Key> input = new List<OpenTK.Input.Key>();
        static RectangleF position = new RectangleF(100f, 100f, 20f, 20f);

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            InputManager.Instance.Initialize(Window);

        }
        public static void Update(object sender, FrameEventArgs e) {

            input.Clear();
            input.AddRange(InputManager.Instance.GetAllKeysDown());
            if (InputManager.Instance.KeyDown(OpenTK.Input.Key.Up) || InputManager.Instance.KeyDown(OpenTK.Input.Key.W)) {
                position.Y -= (float)(50f * e.Time);
            }
            if (InputManager.Instance.KeyDown(OpenTK.Input.Key.Down) || InputManager.Instance.KeyDown(OpenTK.Input.Key.S)) {
                position.Y += (float)(50f * e.Time);
            }
            if (InputManager.Instance.KeyDown(OpenTK.Input.Key.Right) || InputManager.Instance.KeyDown(OpenTK.Input.Key.D)) {
                position.X += (float)(50f * e.Time);
            }
            if (InputManager.Instance.KeyDown(OpenTK.Input.Key.Left) || InputManager.Instance.KeyDown(OpenTK.Input.Key.A)) {
                position.X -= (float)(50f * e.Time);
            }

            InputManager.Instance.Update();
        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);

            GraphicsManager.Instance.DrawRect(position, Color.Red);
            GraphicsManager.Instance.DrawString("Keys down: ", new Point(0, 0), Color.Black);
            GraphicsManager.Instance.DrawString("Space key is up: " + InputManager.Instance.KeyDown(OpenTK.Input.Key.Space), new Point(20, 20), Color.Black);
            GraphicsManager.Instance.DrawString("Space key is down: " + InputManager.Instance.KeyUp(OpenTK.Input.Key.Space), new Point(20, 40), Color.Black);
            GraphicsManager.Instance.DrawString("Space key is pressed: " + InputManager.Instance.KeyPressed(OpenTK.Input.Key.Space), new Point(20, 60), Color.Black);
            GraphicsManager.Instance.DrawString("Space key is released: " + InputManager.Instance.KeyReleased(OpenTK.Input.Key.Space), new Point(20, 80), Color.Black);

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
