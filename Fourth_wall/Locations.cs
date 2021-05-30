using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public static class Locations
    {
        public static Location CreateFirstLocation()
        {
            var enemy1 = Enemies.CreateLightEnemy(450, 75);
            var enemy2 = Enemies.CreateLightEnemy(450, 150);
            var enemy3 = Enemies.CreateLightEnemy(450, 220);
            var enemies = new List<Enemy>() {enemy1, enemy2, enemy3};

            var wall1 = new Wall(0, 0, 20, 300);
            var wall2 = new Wall(0, 0, 500, 20);
            var wall3 = new Wall(480, 0, 20, 300);
            var wall4 = new Wall(0, 280, 500, 20);
            var walls = new List<Wall>() {wall1, wall2, wall3, wall4};

            var box1 = new DestructibleObject(170, 170, 20, 20, 8, new Bitmap(20, 20));
            var destructibleObjects = new List<DestructibleObject>() {box1};
            
            return new Location(enemies, walls, 
            destructibleObjects, new Chest(150, 100),
                 new Hero(75, 225, 6000, new List<Image>(), 4, 20));
        }
    }
}