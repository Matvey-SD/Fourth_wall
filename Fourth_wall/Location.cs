using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Fourth_wall.Game_Objects;

namespace Fourth_wall
{
    public class Location
    {
        public readonly bool IsFirstLocation;
        public readonly List<Enemy> Enemies;
        public readonly IEnumerable<Wall> Walls;
        public readonly List<DestructibleObject> DestructibleObjects;
        public readonly Chest Chest;
        public readonly Hero Hero;
        public readonly Exit Exit;
        private readonly Size _size = new Size(500, 300);
        public bool IsChestOpened = false;
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

        public Location(List<Enemy> enemies, IEnumerable<Wall> walls, 
            List<DestructibleObject> destructibleObjects, Chest chest, Hero hero, bool isFirstLocation, Exit exit)
        {
            Enemies = enemies;
            Walls = walls;
            DestructibleObjects = destructibleObjects;
            Hero = hero;
            IsFirstLocation = isFirstLocation;
            Exit = exit;
            Chest = chest;
        }

        public void SetHero(Hero hero)
        {
            Hero.SetHp(hero.Hp);
            Hero.SetDamageBoost(hero.DamageBoost);
        }

        public void MoveEnemies()
        {
            if (IsHeroInside())
                Parallel.For(0, Enemies.Count(), i => { TryMoveEnemy(Enemies[i]); });
        }

        private void TryMoveEnemy(Enemy enemy)
        {
            if (!enemy.IsDead && enemy.IsTriggered)
            {
                if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.X < Hero.MiddlePoint.X && IsThereSpaceToMove(Directions.Right, enemy, false))
                    enemy.Move(Directions.Right);
                else if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.X > Hero.MiddlePoint.X && IsThereSpaceToMove(Directions.Left, enemy, false)) 
                    enemy.Move(Directions.Left);

                if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.Y > Hero.MiddlePoint.Y && IsThereSpaceToMove(Directions.Up, enemy, false)) 
                    enemy.Move(Directions.Up);
                else if (!enemy.TooCloseToHero(this) && 
                    enemy.MiddlePoint.Y < Hero.MiddlePoint.Y && IsThereSpaceToMove(Directions.Down, enemy, false)) 
                    enemy.Move(Directions.Down);
            }
        }
        
        public void TryMoveHero(Directions direction)
        {
            var isRunning = Keyboard.IsKeyDown(Key.LeftShift);
            if (IsThereSpaceToMove(direction, Hero, isRunning)) Hero.Move(direction, isRunning);
        }

        
        private bool IsThereSpaceToMove(Directions direction, ICreature target, bool isRunning)
        {
            switch (direction)
            {
                case Directions.Down:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X, colliderPoint.Y +
                                                                    (isRunning ? 2*target.Speed : target.Speed))))
                            return false;
                    }
                    break;
                }
                case Directions.Up:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X, colliderPoint.Y - 
                                                                    (isRunning ? 2*target.Speed : target.Speed))))
                            return false;
                    }
                    break;
                }
                case Directions.Left:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X - 
                                                   (isRunning ? 2*target.Speed : target.Speed), colliderPoint.Y)))
                            return false;
                    }
                    break;
                }
                case Directions.Right:
                {
                    foreach (var colliderPoint in target.ColliderBorders())
                    {
                        if (!IsSpaceFree(new Point(colliderPoint.X + 
                                                   (isRunning ? 2*target.Speed : target.Speed), colliderPoint.Y)))
                            return false;
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
            
            foreach (var enemy in Enemies)
            {
                if (!enemy.IsDead && enemy.IsPointInside(point))
                    return false;
            }

            return true;
        }

        public void EnemiesSearchForHero()
        {
            Parallel.For(0, Enemies.Count(), i => { Enemies[i].HeroSearch(Hero); });
        }

        public void EnemiesAttackHero()
        {
            Parallel.For(0, Enemies.Count(), i => { Enemies[i].AttackHero(Hero); });
        }
        
        public bool CanOpenChest()
        {
            return IsAllEnemiesDead && Chest.IsHeroNear(Hero);
        }

        public bool LeavingMap()
        {
            return IsAllEnemiesDead && Exit.IsHeroNear(Hero);
        }

        private bool IsHeroInside() => Hero.MiddlePoint.X <= _size.Width && Hero.MiddlePoint.Y <= _size.Height;
    }
}