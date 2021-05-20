using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Enemies
    {
        public static Enemy CreateLightEnemy(int x, int y) => new Enemy(x, y, 10, new List<Image>());

        public static Enemy CreateLightEnemy(Point location) => new Enemy(location, 10, new List<Image>());
        
        public static Enemy CreateHeavyEnemy(int x, int y) => new Enemy(x, y, 30, new List<Image>());

        public static Enemy CreateHeavyEnemy(Point location) => new Enemy(location, 30, new List<Image>());
    }
}