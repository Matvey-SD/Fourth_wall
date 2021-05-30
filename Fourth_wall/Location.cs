﻿using System.Collections.Generic;
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
        private readonly Size _size = new Size(500, 300);
        public bool IsAllEnemiesDead
        {
            get
            {
                foreach (var enemy in Enemies)
                {
                    if (!enemy.IsDead)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

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

        private void TryMoveEnemy(Enemy enemy)
        {
            if (!enemy.IsDead && enemy.IsTriggered)
            {
                if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.X < Hero.MiddlePoint.X && IsThereSpaceToMove(Directions.Right, enemy))
                    enemy.Move(Directions.Right);
                else if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.X > Hero.MiddlePoint.X && IsThereSpaceToMove(Directions.Left, enemy)) 
                    enemy.Move(Directions.Left);

                if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.Y > Hero.MiddlePoint.Y && IsThereSpaceToMove(Directions.Up, enemy)) 
                    enemy.Move(Directions.Up);
                else if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.Y < Hero.MiddlePoint.Y && IsThereSpaceToMove(Directions.Down, enemy)) 
                    enemy.Move(Directions.Down);
            }
        }
        
        public void TryMoveHero(Directions direction)
        {
            if (IsThereSpaceToMove(direction, Hero)) Hero.Move(direction);
        }

        private bool IsThereSpaceToMove(Directions direction, ICreature target)
        {
            switch (direction)
            {
                case Directions.Down:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X, colliderPoint.Y + 1)))
                        {
                            return false;
                        }
                    }
                    break;
                }
                case Directions.Up:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X, colliderPoint.Y - 1)))
                        {
                            return false;
                        }
                    }
                    break;
                }
                case Directions.Left:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X - 1, colliderPoint.Y)))
                        {
                            return false;
                        }
                    }
                    break;
                }
                case Directions.Right:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X + 1, colliderPoint.Y)))
                        {
                            return false;
                        }
                    }
                    break;
                }
            }

            return true;
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

        public void EnemiesSearchForHero()
        {
            foreach (var enemy in Enemies)
            {
                enemy.HeroSearch(Hero);
            }
        }

        public void EnemiesAttackHero()
        {
            foreach (var enemy in Enemies)
            {
                enemy.AttackHero(Hero);
            }
        }

        private bool IsHeroInside() => Hero.MiddlePoint.X <= _size.Width && Hero.MiddlePoint.Y <= _size.Height;
    }
}