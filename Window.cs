using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;

namespace ConsoleApplication1 {
    class Window {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window

        public static void Initialize(object sender, EventArgs e) {

        }
        public static void Update(object sender, FrameEventArgs e) {

        }
        public static void Render(object sender, FrameEventArgs e) {

        }
        public static void Shutdown(object sender, EventArgs e) {

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
