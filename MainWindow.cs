using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;
using System.Collections;
using System.Collections.Generic;

namespace Skeleton {
    class Program {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            TextureManager.Instance.Initialize(Window);
            SoundManager.Instance.Initialize(Window);
            InputManager.Instance.Initialize(Window);

            GameSingleton.Instance.Initialize();
        }
        public static void Update(object sender, FrameEventArgs e) {
            InputManager.Instance.Update();
            Game.Instance.Update((float)e.Time);
        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);

            Game.Instance.Render();

            int FPS = System.Convert.ToInt32(1.0 / e.Time);
            GraphicsManager.Instance.DrawString("FPS: " + FPS, new PointF(5, 5), Color.Black);
            GraphicsManager.Instance.DrawString("FPS: " + FPS, new PointF(4, 4), Color.White);

            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            Game.Instance.Shutdown();

            InputManager.Instance.Shutdown();
            SoundManager.Instance.Shutdown();
            TextureManager.Instance.Shutdown();
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
