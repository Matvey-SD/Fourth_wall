using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Location
    {
        public readonly List<Enemy> Enemies;
        public readonly List<Wall> Walls;
        public readonly List<DestructibleObject> DestructibleObjects;
        public readonly Chest Chest;
        public readonly Hero Hero;

        public Location(List<Enemy> enemies, List<Wall> walls, List<DestructibleObject> destructibleObjectses, Chest chest, Hero hero)
        {
            Enemies = enemies;
            Walls = walls;
            DestructibleObjects = destructibleObjectses;
            Hero = hero;
            Chest = chest;
        }
    }
}