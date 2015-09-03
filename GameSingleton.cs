using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
    class GameSingleton {
        private static GameSingleton instance = null;

        public static GameSingleton Instance {
            get {
                if (instance == null) {
                    instance = new GameSingleton();
                }
                return instance;
            }
        }

        private GameSingleton(){

        }
        public void Initialize(){
           //load textures and audio
        }
        public void Render(){
            //draw stuff
        }
        public void Update(float deltaTime){
            //update
        }
        public void Shutdown(){
            //unload
        }
    }
}
