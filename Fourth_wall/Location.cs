using System.Collections.Generic;
using System.Drawing;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Location
    {
        public readonly List<Enemy> Enemies;
        public readonly List<Wall> Walls;
        public readonly List<DestructibleObject> DestructibleObject;
        public readonly Chest Chest;
        public readonly Point HeroLocation;

        public Location(List<Enemy> enemies, List<Wall> walls, List<DestructibleObject> destructibleObjects, Chest chest, Point heroLocation)
        {
            Enemies = enemies;
            Walls = walls;
            DestructibleObject = destructibleObjects;
            HeroLocation = heroLocation;
            Chest = chest;
        }
    }
}