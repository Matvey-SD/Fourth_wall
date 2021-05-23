using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Location
    {
        public readonly IEnumerable<Enemy> Enemies;
        public readonly IEnumerable<Wall> Walls;
        public readonly IEnumerable<DestructibleObject> DestructibleObjects;
        public readonly Chest Chest;
        public readonly Hero Hero;

        public Location(IEnumerable<Enemy> enemies, IEnumerable<Wall> walls, 
            IEnumerable<DestructibleObject> destructibleObjectses, Chest chest, Hero hero)
        {
            Enemies = enemies;
            Walls = walls;
            DestructibleObjects = destructibleObjectses;
            Hero = hero;
            Chest = chest;
        }
    }
}