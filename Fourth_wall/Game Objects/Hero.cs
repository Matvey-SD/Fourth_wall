using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;

namespace Fourth_wall.Game_Objects
{
    public class Hero : GameObject, ICreature
    {
        //TODO Stamina change, block, sprint
        
        private int MaxHp => 15;
        private int MaxStamina => 200;
        public bool CanRegenStamina { get; private set; } = false;
        private int _staminaTimer;
        public int Hp { get; private set; }
        public int Stamina { get; private set; }
        private readonly int _damage;
        public int DamageBoost { get; private set; }
        private readonly int _range;
        public int Speed { get; private set;}
        public bool IsDead;
        public Directions LastDirection = Directions.Right;
        public readonly Size Collider = new Size(19, 19);
        public bool IsAttackCooldown;
        public bool IsAttackAnimation { get; private set; }
        public bool CanAttack => !IsAttackCooldown && Stamina >= 50;

        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);
        private int FullDamage => _damage + DamageBoost;
        
        #region Constructor
        public Hero(Point location, int hp, int damage, int range, int speed) : base(location)
        {
            Hp = hp;
            Stamina = MaxStamina;
            _damage = damage;
            _range = range;
            DamageBoost = 0;
            Speed = speed;
        }

        public Hero(int x, int y, int hp, int damage, int range, int speed) : base(x, y)
        {
            Hp = hp;
            Stamina = MaxStamina;
            _damage = damage;
            _range = range;
            DamageBoost = 0;
            Speed = speed;
        }
        #endregion

        public IEnumerable<Point> ColliderBorders()
        {
            yield return Location;
            yield return new Point(Location.X + Collider.Width, Location.Y);
            yield return new Point(Location.X, Location.Y + Collider.Height);
            yield return new Point(Location.X + Collider.Width, Location.Y + Collider.Height);
        }

        public void RegenStamina()
        {
            if (CanRegenStamina && Stamina<= MaxStamina) Stamina += 2;
            if (_staminaTimer < 20)
            {
                _staminaTimer++;
            }
            else CanRegenStamina = true;
        }

        public void Hit(Location level)
        {
            if (Stamina <= 50)
                return;

            ChangeStamina(50);

            foreach (var destructObj in level.DestructibleObjects)
            {
                var x = MiddlePoint.X - destructObj.MiddlePoint.X;
                var y = MiddlePoint.Y - destructObj.MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _range + 
                    (destructObj.Collider.Height + destructObj.Collider.Width + Collider.Height + Collider.Width)/4) 
                    destructObj.HpChange(FullDamage);
            }

            foreach (var enemy in level.Enemies)
            {
                var x = MiddlePoint.X - enemy.MiddlePoint.X;
                var y = MiddlePoint.Y - enemy.MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _range + 
                    (enemy.Collider.Height + enemy.Collider.Width + Collider.Height + Collider.Width)/4)
                    enemy.HpChange(FullDamage);
            }
            
            IsAttackCooldown = true;
            IsAttackAnimation = true;
            
            Task.Run(async () =>
            {
                await Task.Delay(500);
                IsAttackAnimation = false;
                await Task.Delay(500);
                IsAttackCooldown = false;
            });
        }
        
        public void Move(Directions direction, bool isRunning)
        {
            if (Stamina < 2) isRunning = false;

            if (isRunning) ChangeStamina(2);
            switch (direction)
            {
                case Directions.Down:
                    ChangeLocation(0,  (isRunning? 2:1)*Speed);
                    break;
                case Directions.Up:
                    ChangeLocation(0, -(isRunning? 2:1)*Speed);
                    break;
                case Directions.Left:
                    LastDirection = Directions.Left;
                    ChangeLocation(-(isRunning? 2:1)*Speed, 0);
                    break;
                case Directions.Right:
                    LastDirection = Directions.Right;
                    ChangeLocation((isRunning? 2:1)*Speed, 0);
                    break;
            }
        }

        private void ChangeStamina(int value)
        {
            Stamina -= value;
            CanRegenStamina = false;
            _staminaTimer = 0;
        }

        public void HpRemove()
        {
            if (--Hp <= 0 && !IsDead)
            {
                IsDead = true;
            }
        }

        public void SetHp(int value) => Hp = value;

        public void SetDamageBoost(int value) => DamageBoost = value;

        public void BoostDamage() => DamageBoost += 2;

        public void Heal() => Hp = MaxHp;
    }
}