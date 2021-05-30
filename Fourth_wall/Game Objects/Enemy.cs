﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Fourth_wall.Game_Objects
{
    public class Enemy : GameObject, ICreature
    {
        public EnemyType Type { get; private set; }
        private int _hp;
        private readonly int _fov = 100;
        private readonly int _atcRange = 15;
        public bool IsTriggered { get; private set; } = false;
        public bool IsDead { get; private set; } = false;
        private bool _isAttackCooldown = false;
        
        public readonly Size Collider = new Size(19, 19);
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);
        public Point OppositeCorner => new Point(Location.X + Collider.Width, Location.Y + Collider.Height);

        #region Constructor
        public Enemy(Point location, int hp, EnemyType type) : base(location)
        {
            _hp = hp;
            Type = type;
        }

        public Enemy(int x, int y, int hp, EnemyType type) : base(x, y)
        {
            _hp = hp;
            Type = type;
        }
        #endregion
        
        public IEnumerable<Point> ColliderBorders()
        {
            yield return Location;
            yield return new Point(Location.X + Collider.Width, Location.Y);
            yield return new Point(Location.X, Location.Y + Collider.Height);
            yield return new Point(Location.X + Collider.Width, Location.Y + Collider.Height);
        }

        public void HeroSearch(Hero hero)
        {
            if (!IsDead)
            {
                var x = hero.MiddlePoint.X - MiddlePoint.X;
                var y = hero.MiddlePoint.Y - MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _fov) 
                    IsTriggered = true;
            }
        }

        public bool TooCloseToHero(Location location)
        {
            var hero = location.Hero;
            var x = hero.MiddlePoint.X - MiddlePoint.X;
            var y = hero.MiddlePoint.Y - MiddlePoint.Y;
            return Math.Sqrt(x * x + y * y) <= 2 * _atcRange / 3 +
                (hero.Collider.Width + hero.Collider.Height + Collider.Width + Collider.Height) / 4;
        }

        public bool IsPointInside(Point point)
        {
            return (point.X > Location.X && point.X < OppositeCorner.X
                                         && point.Y > Location.Y && point.Y < OppositeCorner.Y);
        }
        
        public void HpChange(int hp)
        {
            _hp -= hp;
            if (_hp <= 0)
                Die();
        }

        private void Die() => IsDead = true;

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.Down:
                    ChangeLocation(0, 1);
                    break;
                case Directions.Up:
                    ChangeLocation(0, -1);
                    break;
                case Directions.Left:
                    ChangeLocation(-1, 0);
                    break;
                case Directions.Right:
                    ChangeLocation(1, 0);
                    break;
            }
        }

        public void AttackHero(Hero hero)
        {
            if (_isAttackCooldown)
                return;

            if (!IsDead)
            {
                var x = hero.MiddlePoint.X - MiddlePoint.X;
                var y = hero.MiddlePoint.Y - MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _atcRange + 
                    (hero.Collider.Width + hero.Collider.Height + Collider.Width + Collider.Height)/4)
                {
                    hero.HpRemove();
                    _isAttackCooldown = true;
                    Task.Run(async () =>
                    {
                        await Task.Delay(3000);
                        _isAttackCooldown = false;
                    });
                }
            }
        }
    }
}