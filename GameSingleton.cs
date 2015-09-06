using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton {
    class Game {
        private static Game instance = null;

        public static Game Instance {
            get {
                if (instance == null) {
                    instance = new Game();
                }
                return instance;
            }
        }

        private Game(){

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
