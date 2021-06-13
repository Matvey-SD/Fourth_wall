using System;
using System.Drawing;
using System.Windows.Forms;
using Fourth_wall.Game_Objects;
using Fourth_wall.Properties;
using System.Windows.Input;

namespace Fourth_wall
{
    public sealed partial class GameStage : Form
    {
        private Location _location;
        private readonly Label _info;

        private bool _isExitByEsc;
        private bool _isPaused;

        public GameStage(Location location)
        {
            DoubleBuffered = true;
            _location = location;
            WindowState = FormWindowState.Maximized;

            FormBorderStyle = FormBorderStyle.None;
            Text = Resources.GameName;
            
            InitializeComponent();
            var tickCounter = new Timer {Interval = 15};
            tickCounter.Tick += Tick;
            tickCounter.Start();

            _info = new Label()
            {
                Location = ScreenCoordinates(new Point(500, 250)),
                Size = ScreenSize(new Size(150, 130)),
                BackColor =  Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(_info);

            SizeChanged += (sender, args) => BackgroundImage = GetScaledImage(ClientSize.Width, ClientSize.Height, BackgroundImage);

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            
            FormClosing += (sender, eventArgs) =>
            {
                if (_isExitByEsc)
                {
                    var result = MessageBox.Show(Resources.On_Exit, Resources.Exit, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        eventArgs.Cancel = true;
                        _isExitByEsc = false;
                        _isPaused = false;
                    }
                }
            };

            SizeChanged += (sender, args) =>
            {
                _info.Location = ScreenCoordinates(new Point(190, 170));
                _info.Size = ScreenSize(new Size(150, 130));
            };

            Closed += (sender, args) => _isPaused = true;
        }
        
        private void Tick(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                ChangeLevel();
                GameRestart();
                ReadInput();
                _location.EnemiesSearchForHero();
                _location.EnemiesAttackHero(_damageSound);
                _location.MoveEnemies();
                _location.Hero.RegenStamina();
                _info.Text = PrintInfo();
                OpenChest();
                Invalidate();
                PlaySounds();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO More animations
            foreach (var wall in _location.Walls)
                PrintWall(e, wall);
            
            PrintExit(e);
            
            foreach (var destructibleObject in _location.DestructibleObjects)
                PrintBox(e, destructibleObject);
            
            PrintChest(e);
            
            foreach (var enemy in _location.Enemies) PrintEnemy(e, enemy);

            PrintHero(e);

            PrintHp(e);
            
            PrintStamina(e);
        }

        private void PrintWall(PaintEventArgs e, Wall wall)
        {
            e.Graphics.DrawImage(_wallImage, 
                new Rectangle(ScreenCoordinates(wall), ScreenSize(wall.Collider)));
        }

        private void PrintExit(PaintEventArgs e)
        {
            if (_location.IsAllEnemiesDead)
                e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Exit.Collider), _holeImage),
                    ScreenCoordinates(_location.Exit));
        }
        
        private void PrintBox(PaintEventArgs e, DestructibleObject destructibleObject)
        {
            e.Graphics.DrawImage(
                destructibleObject.IsDestroyed
                    ? GetScaledImage(ScreenSize(destructibleObject.Collider), _brokenBoxImage)
                    : destructibleObject.isDamaged
                        ? GetScaledImage(ScreenSize(destructibleObject.Collider), _damagedBoxImage)
                        : GetScaledImage(ScreenSize(destructibleObject.Collider), _boxImage),
                new Rectangle(ScreenCoordinates(destructibleObject), ScreenSize(destructibleObject.Collider)));
        }

        private void PrintChest(PaintEventArgs e)
        {
            e.Graphics.DrawImage(
                _location.Chest.IsOpened
                    ? GetScaledImage(ScreenSize(_location.Chest.Collider), _chestOpenedImage)
                    : GetScaledImage(ScreenSize(_location.Chest.Collider), _chestClosedImage), 
                ScreenCoordinates(_location.Chest));
        }

        private void PrintEnemy(PaintEventArgs e, Enemy enemy)
        {
            e.Graphics.DrawImage(
                enemy.IsDead
                    ? GetScaledImage(ScreenSize(enemy.Collider), _deadEnemy)
                    : GetScaledImage(ScreenSize(enemy.Collider), _enemyImage),
                ScreenCoordinates(enemy));
            if (enemy.Type == EnemyType.Heavy && !enemy.IsDead)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Gold), 
                    new Rectangle(ScreenCoordinates(enemy.Location), new Size(ScreenSize(enemy.Collider).Width, 1)));
            }
        }

        private void PrintHero(PaintEventArgs e)
        {
            if (_location.Hero.LastDirection == Directions.Right)
            {
                e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Hero.Collider), 
                        _location.Hero.IsAttackAnimation
                            ?_heroAttackRightImage
                            : _location.Hero.IsStandingAnimation 
                                ? _heroStandRightImage 
                                : _heroRunRightImages[_location.Hero.NextAnimation()]), 
                    ScreenCoordinates(_location.Hero));
            }
            else e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Hero.Collider), 
                    _location.Hero.IsAttackAnimation
                        ?_heroAttackLeftImage
                        : _location.Hero.IsStandingAnimation 
                            ? _heroStandLeftImage 
                            : _heroRunLeftImages[_location.Hero.NextAnimation()]), 
                ScreenCoordinates(_location.Hero));
        }

        private void PrintHp(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 5, 200, 20));
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkRed),
                new Rectangle(5, 5, (200*_location.Hero.Hp)/15, 20));
        }

        private void PrintStamina(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 30, 200, 20));
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue),
                new Rectangle(5, 30, (200*_location.Hero.Stamina)/200, 20));
        }

        private void PlaySounds()
        {
            if (_location.LastIterationLivingMonsters > _location.LivingMonstersCount) _monsterDeathSound.Play();
            _location.LastIterationLivingMonsters = _location.LivingMonstersCount;
            if (_location.LastIterationBoxes > _location.BoxesCount) _boxDestructionSound.Play();
            _location.LastIterationBoxes = _location.BoxesCount;
            
        }

        private Bitmap GetScaledImage(int width, int height, Image image)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
                g.DrawImage(image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
        }
        
        private Bitmap GetScaledImage(Size size, Image image)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage(bmp))
                g.DrawImage(image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
        }

        private Point ScreenCoordinates(GameObject currentObject) =>
            new Point
            {
                X = ClientSize.Width * currentObject.Location.X / 500,
                Y = ClientSize.Height * currentObject.Location.Y / 300
            };
        
        private Point ScreenCoordinates(Point currentObject) =>
            new Point
            {
                X = ClientSize.Width * currentObject.X / 500,
                Y = ClientSize.Height * currentObject.Y / 300
            };

        private Size ScreenSize(Size gameObjectSize) => 
            new Size(ClientSize.Width * gameObjectSize.Width / 500, ClientSize.Height * gameObjectSize.Height / 300);

        private void ReadInput()
        {
            // TODO More Inputs (Block)
            _location.Hero.IsStandingAnimation = true;
            if (Keyboard.IsKeyDown(Key.W))
            {
                _location.TryMoveHero(Directions.Up);
                _location.Hero.IsStandingAnimation = false;
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                _location.TryMoveHero(Directions.Down);
                _location.Hero.IsStandingAnimation = false;
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                _location.TryMoveHero(Directions.Right);
                _location.Hero.IsStandingAnimation = false;
            }
            else if (Keyboard.IsKeyDown(Key.A))
            {
                _location.TryMoveHero(Directions.Left);
                _location.Hero.IsStandingAnimation = false;
            }
                
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                _isExitByEsc = true;
                _isPaused = true;
                Application.Exit();
            }

            if (Keyboard.IsKeyDown(Key.Space) && _location.Hero.CanAttack)
            {
                _location.Hero.Hit(_location);
                _hitSound.Play();
            }
        }

        private void OpenChest()
        {
            if (!_location.IsChestOpened && _location.CanOpenChest())
            {
                _chestSound.Play();
                _location.IsChestOpened = true;
                _isPaused = true;
                var result = MessageBox.Show(Resources.ChestOpen_Message, Resources.OpenChest, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                    _location.Hero.BoostDamage();
                else
                    _location.Hero.Heal();
                _isPaused = false;
            }
        }
        
        private void ChangeLevel()
        {
            if (_location.LeavingMap)
            {
                if (_location.Exit.NextMap == null)
                {
                    _isPaused = true;
                    GameEnd();
                }
                else
                {
                    _location.Exit.NextMap.SetHero(_location.Hero);
                    _location = _location.Exit.NextMap;
                }
            }
        }

        private string PrintInfo()
        {
            if (_location.IsFirstLocation)
                return Resources.Info;
            else return "";
        }
        
        private void GameRestart()
        {
            if (_location.Hero.IsDead)
            {
                _deathSound.PlaySync();
                _isPaused = true;
                var result = MessageBox.Show(Resources.DeathMessage, Resources.GameEnd, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    new MainMenu().Show();
                    Close();
                }
            }
        }

        private void GameEnd()
        {
            var result = MessageBox.Show(Resources.GameEndsText, Resources.GameEnd, MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                new MainMenu().Show();
                Close();
            }
        }
    }
}