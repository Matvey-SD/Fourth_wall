using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall.Game_Objects
{
    public class Hero : GameObject, ICreature
    {
        private int _maxHp;
        public int Hp { get; private set; }
        private int _damage;
        private int _damageBoost;
        private int _range;
        public bool IsDead = false;
        public readonly IEnumerable<Image> Texture;
        public readonly Size Collider = new Size(19, 19);
        private bool _isAttackCooldown = false;
        public Point MiddlePoint => new Point(Location.X + Collider.Height / 2, Location.Y + Collider.Width / 2);
        
        #region Constructor
        public Hero(Point location, int hp, IEnumerable<Image> texture, int damage, int range) : base(location)
        {
            Hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }

        public Hero(int x, int y, int hp, IEnumerable<Image> texture, int damage, int range) : base(x, y)
        {
            Hp = hp;
            Texture = texture;
            _damage = damage;
            _range = range;
            _damageBoost = 0;
            _maxHp = 6;
        }
        #endregion

        public IEnumerable<Point> ColliderBorders()
        {
            yield return Location;
            yield return new Point(Location.X + Collider.Width, Location.Y);
            yield return new Point(Location.X, Location.Y + Collider.Height);
            yield return new Point(Location.X + Collider.Width, Location.Y + Collider.Height);
        }

        public void Hit(Location level)
        {
            if (_isAttackCooldown)
                return;
            
            foreach (var destructObj in level.DestructibleObjects)
            {
                var x = MiddlePoint.X - destructObj.MiddlePoint.X;
                var y = MiddlePoint.Y - destructObj.MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _range + 
                    (destructObj.Collider.Height + destructObj.Collider.Width + Collider.Height + Collider.Width)/4) 
                    destructObj.HpChange(FullDamage());
            }

            foreach (var enemy in level.Enemies)
            {
                var x = MiddlePoint.X - enemy.MiddlePoint.X;
                var y = MiddlePoint.Y - enemy.MiddlePoint.Y;
                if (Math.Sqrt(x * x + y * y) <= _range + 
                    (enemy.Collider.Height + enemy.Collider.Width + Collider.Height + Collider.Width)/4)
                    enemy.HpChange(FullDamage());
            }
            
            _isAttackCooldown = true;
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                _isAttackCooldown = false;
            });
        }
        
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

        public void HpRemove()
        {
            if (--Hp <= 0 && !IsDead)
            {
                IsDead = true;
                Die();
            }
        }

        public void BoostDamage() => _damageBoost += 2;

        public void Heal() => Hp = _maxHp;

        private int FullDamage() => _damage + _damageBoost;

        private void Die()
        {
            var result = MessageBox.Show(Resources.DeathMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}