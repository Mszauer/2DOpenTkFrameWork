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

        static int sound1 = -1;
        static int sound2 = -1;

        public static void Initialize(object sender, EventArgs e) {
            SoundManager.Instance.Initialize(Window);

            sound1 = SoundManager.Instance.LoadMp3("Assets/BGMusic.mp3");
            sound2 = SoundManager.Instance.LoadWav("Assets/SampleSound.wav");

            SoundManager.Instance.PlaySound(sound2);
        }
        public static void Update(object sender, FrameEventArgs e) {
            if (!SoundManager.Instance.IsPlaying(sound1) && !SoundManager.Instance.IsPlaying(sound2)) {
                Console.WriteLine("LOOP");
                SoundManager.Instance.PlaySound(sound1);
            }

            float volume = SoundManager.Instance.GetVolume(sound1);
            if (volume > 0.0f) {
                volume -= 0.25f;
                SoundManager.Instance.SetVolume(sound1, volume);
            }
        }
        public static void Render(object sender, FrameEventArgs e) {

        }
        public static void Shutdown(object sender, EventArgs e) {
            SoundManager.Instance.UnloadSound(sound1);
            SoundManager.Instance.UnloadSound(sound2);

            SoundManager.Instance.Initialize(Window);
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
