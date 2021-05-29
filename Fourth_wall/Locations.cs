using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Locations
    {
        public static Location CreateFirstLocation()
        {
            var enemy1 = Enemies.CreateLightEnemy(450, 75);
            var enemy2 = Enemies.CreateLightEnemy(450, 150);
            var enemy3 = Enemies.CreateLightEnemy(450, 220);
            return new Location(new List<Enemy>() {enemy1, enemy2, enemy3}, new List<Wall>(), 
            new List<DestructibleObject>(), new Chest(150, 100),
                 new Hero(75, 225, 6, new List<Image>(), 4, 20));
        }
    }
}