using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;

namespace ConsoleApplication1 {
    class MainWindow {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window
        static int texBird = -1;
        static int texJack = -1;
        static int texAku = -1;
        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            TextureManager.Instance.Initialize(Window);

            texBird = TextureManager.Instance.LoadTexture("Assets/Bird.png");
            texJack = TextureManager.Instance.LoadTexture("Assets/Jack.png");
            texAku = TextureManager.Instance.LoadTexture("Assets/Aku.png");
        }
        public static void Update(object sender, FrameEventArgs e) {

        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);
            TextureManager.Instance.Draw(texAku, new Point(0, 0));
            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            TextureManager.Instance.UnloadTexture(texBird);
            TextureManager.Instance.UnloadTexture(texJack);
            TextureManager.Instance.UnloadTexture(texAku);
            texBird = texAku = texJack = -1;
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
