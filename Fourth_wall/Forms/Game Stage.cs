using System;
using System.Drawing;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public partial class GameStage : Form
    {
        private Location _location;
        private Image _heroImage;
        private Label HP;
        public GameStage(Location location)
        {
            DoubleBuffered = true;
            _location = location;
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\FirstLocation.png");
            
            _heroImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hero.png");
            //var heroDraw = Graphics.FromImage(new Bitmap(50, 50));
            //heroDraw.DrawImage(_heroImage, ScreenCoordinates(_location.Hero));
            

            FormBorderStyle = FormBorderStyle.None;
            Text = "4-th Wall";
            
            InitializeComponent();
            var tickCounter = new Timer {Interval = 10};
            tickCounter.Tick += Tick;
            tickCounter.Start();
            
            HP = new Label {Text = _location.Hero.Hp.ToString(), Location = new Point(0, 0)};
            Controls.Add(HP);

            KeyPress += GameStage_KeyPress;
            
            SizeChanged += (sender, args) => BackgroundImage = GetFormBackgroundImage();

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
            
            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
            
            Paint += (sender, args) =>
            {
                args.Graphics.DrawImage(_heroImage, ScreenCoordinates(_location.Hero));
                args.Graphics.DrawRectangle(new Pen(Color.Blue), 
                    new Rectangle(ScreenCoordinates(_location.Hero), new Size(50, 50)));
            };
        }

        private Point ScreenCoordinates(GameObject currentObject) =>
            new Point
            {
                X = ClientSize.Width * currentObject.Location.X / 500,
                Y = ClientSize.Height * currentObject.Location.Y / 300
            };

        private void Tick(object sender, EventArgs e)
        {
            _location.EnemiesSearchForHero();
            _location.EnemiesAttackHero();
            _location.MoveEnemies();
            HP.Text = _location.Hero.Hp.ToString();
            Update();
        }
        
        private Bitmap GetFormBackgroundImage()
        {
            Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.DrawImage(BackgroundImage,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
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
            /*var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.,
                MessageBoxIcon.Question);*/
            var result = MessageBox.Show("bla bla bla", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, true);
        }
    }
}