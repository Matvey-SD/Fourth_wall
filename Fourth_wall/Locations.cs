using System.Collections.Generic;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public static class Locations
    {
        // TODO New locations and restruct current
        public static Location CreateFirstLocation()
        {
            var enemies = new List<Enemy>()
            {
                /*Enemies.CreateLightEnemy(450, 75), 
                Enemies.CreateLightEnemy(450, 150), */
                Enemies.CreateLightEnemy(450, 220),
                Enemies.CreateLightEnemy(90, 40),
                Enemies.CreateLightEnemy(370, 221)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
                new Wall(190, 170, 150, 130),
                new Wall(190, 20, 150, 80),
                new Wall(190, 10, 20, 90),
                new Wall(10, 170, 60, 20),
                new Wall(130, 170, 60, 20),
                new Wall(330, 170, 70, 20),
                new Wall(430, 170, 70, 20),
            };

            var destructibleObjects = new List<DestructibleObject>()
            {
                new DestructibleObject(110, 170, 20, 20, 8),
                new DestructibleObject(90, 170, 20, 20, 8),
                new DestructibleObject(70, 170, 20, 20, 8)
            };

            var chest = new Chest(410, 260);

            var hero = new Hero(90, 250, 15, 6);

            var exit = new Exit(410, 0, CreateSecondLocation());
            
            return new Location(enemies, walls, 
            destructibleObjects, chest,
            hero, true, exit);
        }
        
        public static Location CreateSecondLocation()
        {
            var enemies = new List<Enemy>()
            {
                Enemies.CreateLightEnemy(350, 90), 
                Enemies.CreateHeavyEnemy(410, 150), 
                Enemies.CreateLightEnemy(351, 205),
                Enemies.CreateLightEnemy(180, 100),
                Enemies.CreateLightEnemy(179, 60)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
                new Wall(10, 170, 100, 120),
                new Wall(90, 10, 20, 120),
                new Wall(210, 10, 20, 100),
                new Wall(210, 190, 20, 100)
            };

            var destructibleObjects = new List<DestructibleObject>()
            {
                new DestructibleObject(110, 190, 100, 90, 80)
            };

            var chest = new Chest(450, 160);

            var hero = new Hero(50, 75, 15, 6);

            var exit = new Exit(450, 0, CreateThirdLocation());
            
            return new Location(enemies, walls, 
                destructibleObjects, chest,
                hero, false, exit);
        }
        
        public static Location CreateThirdLocation()
        {
            var enemies = new List<Enemy>()
            {
                Enemies.CreateBoss(420, 130)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
            };

            var destructibleObjects = new List<DestructibleObject>();

            var chest = new Chest(700, 150);

            var hero = new Hero(80, 150, 15, 10);

            var exit = new Exit(450, 0);
            
            return new Location(enemies, walls, 
                destructibleObjects, chest,
                hero, false, exit);
        }
    }
}