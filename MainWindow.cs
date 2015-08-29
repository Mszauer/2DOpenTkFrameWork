using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;

namespace ConsoleApplication1 {
    class MainWindow {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window
        static float currentRotation = 0.0f;
        static int texRobot = -1;

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            TextureManager.Instance.Initialize(Window);

            texRobot = TextureManager.Instance.LoadTexture("Assets/machine.png");
        }
        public static void Update(object sender, FrameEventArgs e) {
            currentRotation += (float)e.Time * 60f;
            while (currentRotation > 360.0f){
                currentRotation -= 360.0f;
            }
        }
        public static void Render(object sender, FrameEventArgs e) {
            GraphicsManager.Instance.ClearScreen(Color.CadetBlue);

            TextureManager.Instance.Draw(texRobot, new Point(212, 85), 0.75f, new Rectangle(64, 0, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(172, 85), new PointF(-0.75f, .75f), new Rectangle(64, 0, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(320, 20));
            TextureManager.Instance.Draw(texRobot, new Point(128, 0), 1.0f, new Rectangle(128, 0, 128, 128));
            TextureManager.Instance.Draw(texRobot, new Point(168, 31), 0.75f, new Rectangle(0, 64, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(219, 41), 0.75f, new Rectangle(64, 64, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(165, 41), new PointF(-0.75f,0.75f), new Rectangle(64, 64, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(224, 111), new PointF(0.75f, -0.75f), new Rectangle(64, 0, 64, 64));
            TextureManager.Instance.Draw(texRobot, new Point(159, 111), 0.75f, new Rectangle(64, 0, 64, 64), new Point(0,0),180);
            TextureManager.Instance.Draw(texRobot, new Point(226, 75), 0.75f, new Rectangle(0, 0, 64, 64), currentRotation);
            TextureManager.Instance.Draw(texRobot, new Point(157, 75), new PointF(-0.75f, 0.75f), new Rectangle(0, 0, 64, 64), currentRotation);
            
            GraphicsManager.Instance.SwapBuffers();
        }
        public static void Shutdown(object sender, EventArgs e) {
            TextureManager.Instance.UnloadTexture(texRobot);
            texRobot = -1;
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
