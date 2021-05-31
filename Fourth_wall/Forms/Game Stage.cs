﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Fourth_wall.Game_Objects;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public sealed partial class GameStage : Form
    {
        private Location _location;

        private readonly Label _hp;
        private readonly Label _info;

        private bool _isExitByEsc;
        private bool _isGamesEndsMessage;
        private bool _isHidden;

        public GameStage(Location location)
        {
            DoubleBuffered = true;
            _location = location;
            WindowState = FormWindowState.Maximized;

            FormBorderStyle = FormBorderStyle.None;
            Text = Resources.GameName;
            
            InitializeComponent();
            var tickCounter = new Timer {Interval = 10};
            tickCounter.Tick += Tick;
            tickCounter.Start();
            
            _hp = new Label
            {
                Location = new Point(0, 0), 
                BackColor = Color.Transparent,
                ForeColor = Color.DarkRed
            };
            Controls.Add(_hp);

            _info = new Label()
            {
                Location = ScreenCoordinates(new Point(500, 250)),
                Size = ScreenSize(new Size(150, 130)),
                BackColor =  Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(_info);
            

            KeyPress += GameStage_KeyPress;
            
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
                    }
                }
            };

            SizeChanged += (sender, args) =>
            {
                _info.Location = ScreenCoordinates(new Point(190, 170));
                _info.Size = ScreenSize(new Size(150, 130));
            };

            Closed += (sender, args) => _isHidden = true;
        }
        
        private void Tick(object sender, EventArgs e)
        {
            if (!_isHidden)
            {
                if (!_location.Hero.IsDead)
                {
                    ChangeLocation();
                    _location.EnemiesSearchForHero();
                    _location.EnemiesAttackHero();
                    _location.MoveEnemies();
                    _hp.Text = _location.Hero.Hp.ToString();
                    _info.Text = PrintInfo();
                    OpenChest();
                    Invalidate(); 
                }
                else
                    GameRestart();
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
                            ?_heroAttackImage
                            : _heroImage), 
                    ScreenCoordinates(_location.Hero));
            }
            else e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Hero.Collider), 
                    _location.Hero.IsAttackAnimation
                        ?_heroAttackLeftImage
                        : _heroLeftImage), 
                ScreenCoordinates(_location.Hero));
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

        private void GameStage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // TODO More Inputs (Diagonal movement, block, sprint)
            switch (e.KeyChar)
            {
                case 'w' :
                    _location.TryMoveHero(Directions.Up);
                    break;
                case 's' :
                    _location.TryMoveHero(Directions.Down);
                    break;
                case 'd' :
                    _location.TryMoveHero(Directions.Right);   
                    break;
                case 'a' :
                    _location.TryMoveHero(Directions.Left);
                    break;
                case (char)27:          //ESC
                    _isExitByEsc = true; 
                    Application.Exit();
                    break;
                case (char)32:          //SPACE
                    _location.Hero.Hit(_location);
                    break;
            }
        }

        private void OpenChest()
        {
            if (!_location.IsChestOpened && _location.CanOpenChest())
            {
                _location.IsChestOpened = true;
                var result = MessageBox.Show(Resources.ChestOpen_Message, Resources.OpenChest, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                    _location.Hero.BoostDamage();
                else
                    _location.Hero.Heal();
            }
        }
        
        private void ChangeLocation()
        {
            if (_location.LeavingMap())
            {
                if (_location.Exit.NextMap == null) GameEnd();
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
            new MainMenu().Show();
            Close();
        }

        private void GameEnd()
        {
            if (!_isGamesEndsMessage)
            {
                _isGamesEndsMessage = true;
                var result = MessageBox.Show(Resources.GameEndsText, Resources.GameEnd, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    new MainMenu().Show();
                    Close();
                }
            }
        }
    }
}