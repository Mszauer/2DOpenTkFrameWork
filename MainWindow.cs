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
        static int texSwirl = -1;
        static float swirlAngle = 0.0f;

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            TextureManager.Instance.Initialize(Window);

            texBird = TextureManager.Instance.LoadTexture("Assets/Bird.png");
            texJack = TextureManager.Instance.LoadTexture("Assets/Jack.png");
            texAku = TextureManager.Instance.LoadTexture("Assets/Aku.png");
            texSwirl = TextureManager.Instance.LoadTexture("Assets/Swirl.png");
        }
        public static void Update(object sender, FrameEventArgs e) {
            swirlAngle += (float)e.Time * 60f;
            while (swirlAngle > 360.0f){
                swirlAngle -= 360.0f;
            }
        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);

            TextureManager.Instance.Draw(texAku, new Point(0, 0));
            TextureManager.Instance.Draw(texBird, new Point(0, 0), 0.25f);
            TextureManager.Instance.Draw(texBird, new Point(50, 0), 0.5f);
            TextureManager.Instance.Draw(texBird, new Point(170, 30), 0.75f);
            TextureManager.Instance.Draw(texJack, new Point(0, 288), 1.0f, new Rectangle(0, 200, 256, 312));
            TextureManager.Instance.Draw(texSwirl, new Point(300, 450), 0.25f, new Rectangle(0, 0, 512, 512), swirlAngle);
            TextureManager.Instance.Draw(texSwirl, new Point(275, 325), 0.1f, new Rectangle(0, 0, 512, 512), new Point(0, 0), -swirlAngle);

            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            TextureManager.Instance.UnloadTexture(texBird);
            TextureManager.Instance.UnloadTexture(texJack);
            TextureManager.Instance.UnloadTexture(texAku);
            TextureManager.Instance.UnloadTexture(texSwirl);
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
