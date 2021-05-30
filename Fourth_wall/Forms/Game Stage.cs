using System;
using System.Drawing;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public sealed partial class GameStage : Form
    {
        private Location _location;
        private Image _heroImage;
        private Image _enemyImage;
        private Label _hp;
        
        public GameStage(Location location)
        {
            DoubleBuffered = true;
            _location = location;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\FirstLocation.png");
            
            _heroImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hero.png");
            _enemyImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Enemy.png");


            FormBorderStyle = FormBorderStyle.None;
            Text = Resources.GameName;
            
            InitializeComponent();
            var tickCounter = new Timer {Interval = 10};
            tickCounter.Tick += Tick;
            tickCounter.Start();
            
            _hp = new Label {Text = _location.Hero.Hp.ToString(), Location = new Point(0, 0), BackColor = Color.Transparent};
            Controls.Add(_hp);

            KeyPress += GameStage_KeyPress;
            
            SizeChanged += (sender, args) => BackgroundImage = GetScaledImage(ClientSize.Width, ClientSize.Height, BackgroundImage);

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            
            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(GetScaledImage(ScreenSize(_location.Hero.Collider), _heroImage), ScreenCoordinates(_location.Hero));
            foreach (var enemy in _location.Enemies) 
                e.Graphics.DrawImage(GetScaledImage(ScreenSize(enemy.Collider), _enemyImage), ScreenCoordinates(enemy));
            foreach (var destructibleObject in _location.DestructibleObjects)
                e.Graphics.DrawRectangle(new Pen(Color.Khaki),
                    new Rectangle(ScreenCoordinates(destructibleObject), ScreenSize(destructibleObject.Collider)));
            foreach (var wall in _location.Walls)
                e.Graphics.DrawRectangle(new Pen(Color.Brown), 
                    new Rectangle(ScreenCoordinates(wall), ScreenSize(wall.Collider)));
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

        private Size ScreenSize(Size gameObjectSize) => 
            new Size(ClientSize.Width * gameObjectSize.Width / 500, ClientSize.Height * gameObjectSize.Height / 300);

        private void Tick(object sender, EventArgs e)
        {
            if (!_location.Hero.IsDead )
            {
                _location.EnemiesSearchForHero();
                _location.EnemiesAttackHero();
                _location.MoveEnemies();
                _hp.Text = _location.Hero.Hp.ToString();
                Invalidate(); 
            }
        }

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
                    Application.Exit();
                    break;
                case (char)32:          //SPACE
                    _location.Hero.Hit(_location);
                    break;
            }
        }

        private void OpenChest()
        {
            if (_location.Chest.CanOpenChest(_location))
            {
                var result = MessageBox.Show("bla bla bla", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, true);
            }
        }
    }
}