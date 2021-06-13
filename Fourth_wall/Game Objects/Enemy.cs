using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;

namespace Fourth_wall.Game_Objects
{
    public class Enemy : GameObject, ICreature
    {
        private int _hp;
        public int Speed { get; }
        private readonly int _fov = 100;
        private readonly int _atcRange = 2;
        private EnemyType EnemyType;
        public Directions LastDirection = Directions.Left;
        public bool IsAttackAnimation { get; private set; }
        public bool IsStandingAnimation { get; set; } = true;
        private int AnimationCounter { get; set; }
        public bool IsTriggered { get; private set; }
        public bool IsDead { get; private set; }
        private bool _isAttackCooldown;
        
        public Size Collider { get; }
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);
        private Point OppositeCorner => new Point(Location.X + Collider.Width, Location.Y + Collider.Height);

        #region Constructor
        
        public Enemy(int x, int y, EnemyType type) : base(x, y)
        {
            EnemyType = type;
            switch (type)
            {
                case EnemyType.Light:
                    _hp = 14;
                    Speed = 2;
                    Collider = new Size(19, 19);
                    break;
                case EnemyType.Heavy:
                    _hp = 28;
                    Speed = 1;
                    Collider = new Size(25, 25);
                    break;
                case EnemyType.Boss:
                    _hp = 54;
                    Speed = 1;
                    Collider = new Size(40, 40);
                    break;
                default:
                    _hp = 1;
                    Speed = 1;
                    Collider = new Size(19, 19);
                    break;
            }
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
            var value = Math.Sqrt(x * x + y * y) <= 2 * _atcRange / 3 +
                (hero.Collider.Width + hero.Collider.Height + Collider.Width + Collider.Height) / 2;
            if (value) IsStandingAnimation = true;
            return value;
        }

        public bool IsPointInside(Point point)
        {
            return (point.X > Location.X && point.X < OppositeCorner.X
                                         && point.Y > Location.Y && point.Y < OppositeCorner.Y);
        }
        
        public void HpChange(int hp)
        {
            _hp -= hp;
            if (_hp <= 0) Die();

        }

        private void Die() => IsDead = true;
        
        public void Move(Directions direction)
        {
            IsStandingAnimation = false;
            switch (direction)
            {
                case Directions.Down:
                    ChangeLocation(0, Speed);
                    break;
                case Directions.Up:
                    ChangeLocation(0, -Speed);
                    break;
                case Directions.Left:
                    LastDirection = Directions.Left;
                    ChangeLocation(-Speed, 0);
                    break;
                case Directions.Right:
                    LastDirection = Directions.Right;
                    ChangeLocation(Speed, 0);
                    break;
            }
        }

        public void AttackHero(Hero hero, SoundPlayer damage)
        {
            if (_isAttackCooldown)
                return;

            if (!IsDead)
            {
                var x = hero.MiddlePoint.X - MiddlePoint.X;
                var y = hero.MiddlePoint.Y - MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _atcRange + 
                    (hero.Collider.Width + hero.Collider.Height + Collider.Width + Collider.Height)/2)
                {
                    damage.Play();
                    hero.HpRemove();
                    _isAttackCooldown = true;
                    IsAttackAnimation = true;
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);
                        IsAttackAnimation = false;
                        await Task.Delay(EnemyType == EnemyType.Boss ? 300 : 2800);
                        _isAttackCooldown = false;
                    });
                }
            }
        }
        
        public int NextAnimation()
        {
            var value = AnimationCounter / 5;
            AnimationCounter = (AnimationCounter + 1) % 40;
            return value;
        }
    }
}