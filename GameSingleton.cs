using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameFramework;

namespace Skeleton {
    class Game {
        protected Tile[][] map = null;
        protected int[][] mapLayout = new int[][] {
            new int[] {1,1,1,1,1,1,1,1 },
            new int[] {1,0,0,0,0,0,0,1 },
            new int[] {1,0,1,0,0,0,0,1 },
            new int[] {1,0,0,0,0,1,0,1 },
            new int[] {1,0,0,0,0,0,0,1 },
            new int[] {1,1,1,1,1,1,1,1 }
        };
        protected string[] spriteSheets = new string[] {
            "Assets/HouseTiles.png",
            "Assets/HouseTiles.png"
        };
        protected Rectangle[] spriteSources = new Rectangle[] {
            new Rectangle(466,32,30,30),
            new Rectangle(466,1,30,30)
        };

        Tile[][] GenerateMap(int[][] layout, string[] sheets, Rectangle[] sources) {
            Tile[][] result = new Tile[layout.Length][];
            float scale = 1.0f;
            for (int i = 0; i < layout.Length; i++) {
                result[i] = new Tile[layout[i].Length];

                for (int j = 0; j < layout[i].Length; j++) {
                    //the 0's and i's in the layout array corrrespond to
                    //strings and rectangles in sheets and sources array
                    string sheet = sheets[layout[i][j]];
                    Rectangle source = sources[layout[i][j]];
                    //we dont have to take scale into account, but we do need
                    //to space our grid out accordingly. It might be smart
                    //to have a width and height that's a constant, right now
                    //we are assuming that all of the passed in rects in sources
                    //will hav a uniform size
                    Point worldPosition = new Point();
                    worldPosition.X=(int)(j * source.Width);
                    worldPosition.Y = (int)(i * source.Height);

                    result[i][j] = new Tile(sheet, source);
                    result[i][j].Walkable = layout[i][j] == 0;
                    result[i][j].WorldPosition = worldPosition;
                    result[i][j].scale = scale;
                }
            }
            return result;
        }

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
            TextureManager.Instance.UseNearestFiltering = true; //this doesn't exist? the UseNearestFiltering
            map = GenerateMap(mapLayout, spriteSheets, spriteSources);
        }
        public void Render(){
            for (int h = 0; h < map.Length; h++) {
                for (int w = 0; w < map[h].Length; w++) {
                    map[h][w].Render();
                }
            }
        }
        public void Update(float deltaTime){
            //update
        }
        public void Shutdown(){
            for (int i = 0; i < map.Length; i++) {
                for (int j = 0; j < map[i].Length; j++) {
                    map[i][j].Destroy();
                }
            }
        }
    }
}
