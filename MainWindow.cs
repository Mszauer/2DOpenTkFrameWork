using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;

namespace ConsoleApplication1 {
    class MainWindow {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window
        static int robot = -1;
        static int currentRotation = 0;
        public static void Initialize(object sender, EventArgs e) {
            robot = TextureManager.Instance.LoadTexture("Assets/machine.png");
            GraphicsManager.Instance.Initialize(Window);
            TextureManager.Instance.Initialize(Window);
        }
        public static void Update(object sender, FrameEventArgs e) {

        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);
            TextureManager.Instance.Draw(robot, new Point(0, 0));
            /*
            //draw each piece individually
            TextureManager.Instance.Draw(robot, new Point(128, 0), 1.0f, new Rectangle(128, 0, 128, 128));
            TextureManager.Instance.Draw(robot, new Point(168, 31), .75f, new Rectangle(0, 64, 64, 64));

             */
            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            TextureManager.Instance.UnloadTexture(robot);
            robot = -1;
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

            //how do i call the methods?

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
