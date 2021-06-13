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
                Enemies.CreateLightEnemy(450, 75), 
                Enemies.CreateLightEnemy(450, 150), 
                Enemies.CreateLightEnemy(450, 220)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
                new Wall(190, 170, 150, 130),
                new Wall(190, 10, 20, 90)
            };

            var destructibleObjects = new List<DestructibleObject>()
            {
                new DestructibleObject(170, 170, 20, 20, 8),
                new DestructibleObject(150, 170, 20, 20, 8),
                new DestructibleObject(130, 170, 20, 20, 8),
                new DestructibleObject(110, 170, 20, 20, 8),
                new DestructibleObject(90, 170, 20, 20, 8),
                new DestructibleObject(70, 170, 20, 20, 8),
                new DestructibleObject(50, 170, 20, 20, 8),
                new DestructibleObject(30, 170, 20, 20, 8),
                new DestructibleObject(10, 170, 20, 20, 8)
            };

            var chest = new Chest(450, 260);

            var hero = new Hero(75, 250, 15, 6, 20, 2);

            var exit = new Exit(450, 0, CreateSecondLocation());
            
            return new Location(enemies, walls, 
            destructibleObjects, chest,
            hero, true, exit);
        }
        
        public static Location CreateSecondLocation()
        {
            var enemies = new List<Enemy>()
            {
                Enemies.CreateLightEnemy(450, 75), 
                Enemies.CreateHeavyEnemy(450, 150), 
                Enemies.CreateLightEnemy(450, 220),
                Enemies.CreateLightEnemy(210, 190),
                Enemies.CreateLightEnemy(270, 190)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
                new Wall(160, 0, 20, 200),
                new Wall(320, 100, 20, 200)
            };

            var destructibleObjects = new List<DestructibleObject>()
            {
                new DestructibleObject(300, 100, 20, 20, 8),
                new DestructibleObject(280, 100, 20, 20, 8),
                new DestructibleObject(260, 100, 20, 20, 8),
                new DestructibleObject(240, 100, 20, 20, 8),
                new DestructibleObject(220, 100, 20, 20, 8),
                new DestructibleObject(200, 100, 20, 20, 8),
                new DestructibleObject(180, 100, 20, 20, 8),
                new DestructibleObject(160, 200, 20, 20, 8),
                new DestructibleObject(160, 220, 20, 20, 8),
                new DestructibleObject(160, 240, 20, 20, 8),
                new DestructibleObject(160, 260, 20, 20, 8)
            };

            var chest = new Chest(450, 260);

            var hero = new Hero(30, 150, 15, 6, 20, 1);

            var exit = new Exit(450, 0, CreateThirdLocation());
            
            return new Location(enemies, walls, 
                destructibleObjects, chest,
                hero, false, exit);
        }
        
        public static Location CreateThirdLocation()
        {
            var enemies = new List<Enemy>()
            {
                Enemies.CreateBoss(420, 150)
            };
            
            var walls = new List<Wall>()
            {
                new Wall(0, 0, 10, 300), 
                new Wall(0, 0, 500, 20), 
                new Wall(490, 0, 10, 300), 
                new Wall(0, 280, 500, 20),
            };

            var destructibleObjects = new List<DestructibleObject>();

            var chest = new Chest(450, 150);

            var hero = new Hero(80, 250, 15, 6, 20, 1);

            var exit = new Exit(450, 0);
            
            return new Location(enemies, walls, 
                destructibleObjects, chest,
                hero, false, exit);
        }
    }
}