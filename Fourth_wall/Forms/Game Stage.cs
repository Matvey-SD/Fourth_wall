using System;
using System.Drawing;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public sealed partial class GameStage : Form
    {
        public Location _location;
        
        private Image _heroImage;
        private Image _enemyImage;
        private Image _deadEnemy;
        private Image _wallImage;
        private Image _boxImage;
        private Image _brokenBoxImage;
        private Image _chestClosedImage;
        private Image _chestOpenedImage;
        private Image _holeImage;
        
        private Label _hp;
        private Label _info;

        private bool _isExitByEsc = false;
        private bool _isGamesEndsMessage = false;
        private bool _isHidden = false;

        public GameStage(Location location)
        {
            DoubleBuffered = true;
            _location = location;
            WindowState = FormWindowState.Maximized;
            
            BackgroundImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Floor.png");
            _heroImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hero.png");
            _enemyImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Enemy.png");
            _deadEnemy = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\DeadEnemy.png");
            _wallImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Wall.png");
            _boxImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Box.png");
            _brokenBoxImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\BoxBroken.png");
            _chestClosedImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\ChestClosed.png");
            _chestOpenedImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\ChestOpened.png");
            _holeImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hole.png");
            
            FormBorderStyle = FormBorderStyle.None;
            Text = Resources.GameName;
            
            InitializeComponent();
            var tickCounter = new Timer {Interval = 10};
            tickCounter.Tick += Tick;
            tickCounter.Start();
            
            _hp = new Label {Location = new Point(0, 0), BackColor = Color.Transparent};
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
                    _location.Chest.CanOpenChest(_location);
                    OpenChest();
                    Invalidate(); 
                }
                else
                    GameRestart();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var wall in _location.Walls)
                e.Graphics.DrawImage(_wallImage, 
                    new Rectangle(ScreenCoordinates(wall), ScreenSize(wall.Collider)));
            
            if (_location.IsAllEnemiesDead)
                e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Exit.Collider), _holeImage),
                    ScreenCoordinates(_location.Exit));
            
            foreach (var destructibleObject in _location.DestructibleObjects)
                e.Graphics.DrawImage(
                    destructibleObject.IsDestroyed
                        ? GetScaledImage(ScreenSize(destructibleObject.Collider), _brokenBoxImage)
                        : GetScaledImage(ScreenSize(destructibleObject.Collider), _boxImage),
                    new Rectangle(ScreenCoordinates(destructibleObject), ScreenSize(destructibleObject.Collider)));
            
            e.Graphics.DrawImage(
                _location.Chest.IsOpened
                    ? GetScaledImage(ScreenSize(_location.Chest.Collider), _chestOpenedImage)
                    : GetScaledImage(ScreenSize(_location.Chest.Collider), _chestClosedImage), 
                ScreenCoordinates(_location.Chest));
            
            foreach (var enemy in _location.Enemies)
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
            
            e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Hero.Collider), _heroImage), ScreenCoordinates(_location.Hero));
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