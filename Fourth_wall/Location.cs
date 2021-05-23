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
        public readonly Size Size = new Size(1000, 563);

        public Location(IEnumerable<Enemy> enemies, IEnumerable<Wall> walls, 
            IEnumerable<DestructibleObject> destructibleObjectses, Chest chest, Hero hero)
        {
            Enemies = enemies;
            Walls = walls;
            DestructibleObjects = destructibleObjectses;
            Hero = hero;
            Chest = chest;
        }

        public void MoveEnemies()
        {
            if (IsHeroInside())
                foreach (var enemy in Enemies)
                    TryMoveEnemy(enemy);
        }

        public void TryMoveHero(Directions direction)
        {
            if (IsThereSpaceToMove(direction, Hero)) Hero.Move(direction);
        }

        private void TryMoveEnemy(Enemy enemy)
        {
            if (!enemy.IsDead && enemy.IsTriggered)
            {
                if (enemy.Location.X < Hero.Location.X && IsThereSpaceToMove(Directions.Right, enemy))
                    enemy.Move(Directions.Right);

                if (enemy.Location.X > Hero.Location.X && IsThereSpaceToMove(Directions.Left, enemy)) 
                    enemy.Move(Directions.Left);

                if (enemy.Location.Y > Hero.Location.Y && IsThereSpaceToMove(Directions.Up, enemy)) 
                    enemy.Move(Directions.Up);

                if (enemy.Location.Y > Hero.Location.Y && IsThereSpaceToMove(Directions.Down, enemy)) 
                    enemy.Move(Directions.Down);
            }
        }

        private bool IsThereSpaceToMove(Directions direction, GameObject target)
        {
            switch (direction)
            {
                case Directions.Down:
                    return IsSpaceFree(new Point(target.Location.X, target.Location.Y + 1));
                case Directions.Up:
                    return IsSpaceFree(new Point(target.Location.X, target.Location.Y - 1));
                case Directions.Left:
                    return IsSpaceFree(new Point(target.Location.X - 1, target.Location.Y));
                case Directions.Right:
                    return IsSpaceFree(new Point(target.Location.X + 1, target.Location.Y));
            }

            return false;
        }

        private bool IsSpaceFree(Point point)
        {
            foreach (var wall in Walls)
            {
                if (wall.IsPointInside(point))
                    return false;
            }

            foreach (var destructibleObject in DestructibleObjects)
            {
                if (!destructibleObject.IsDestroyed && destructibleObject.IsPointInside(point))
                    return false;
            }

            return true;
        }

        private bool IsHeroInside() => Hero.Location.X <= Size.Width && Hero.Location.Y <= Size.Height;
    }
}