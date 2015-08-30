using System;
using OpenTK;
using OpenTK.Input;
using System.Drawing;
using GameFramework;

namespace ConsoleApplication1 {
    class MainWindow {
        public static OpenTK.GameWindow Window = null; //reference to OpenTK window

        public static int MouseX { get; }
        public static int MouseY {get;}
        public Point MousePosition{get;}

        public float MouseDeltaX { get; }
        public float MouseDeltaY { get; }
        public PointF MouseDelta { get; }

        public void SetMousePosition(Point newPos) {

        }
        public void CenterMouse() {

        }

        public bool MouseDown(OpenTK.Input.MouseButton button) {
            
            if (button == MouseButton.Left|| button == MouseButton.Right || button == MouseButton.Middle) {
                return true;
            }
            return false;
        }
        public bool MouseUp(OpenTK.Input.MouseButton button) {

        }
        public bool MousePressed(OpenTK.Input.MouseButton button) {

        }
        public bool MouseReleased(OpenTK.Input.MouseButton button) {

        }

        public static void Initialize(object sender, EventArgs e) {
            GraphicsManager.Instance.Initialize(Window);
            InputManager.Instance.Initialize(Window);

        }
        public static void Update(object sender, FrameEventArgs e) {
            InputManager.Instance.Update();
        }
        public static void Render(object sender, FrameEventArgs e) {
            int FPS = System.Convert.ToInt32(1.0 / e.Time);
            GraphicsManager.Instance.DrawString("FPS: " + FPS, new Point(5, 5), Color.Black);
            GraphicsManager.Instance.DrawString("FPS: " + FPS, new Point(4, 4), Color.White);
            GraphicsManager.Instance.DrawString("Mouse X: " + MouseX + " Y: " + MouseY, new Point(8,8), Color.Black);

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
