using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public sealed partial class MainMenu : Form
    {
        private Bitmap GetFormBackgroundImage()
        {
            Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.DrawImage(BackgroundImage,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
        }
        
        public MainMenu()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            BackgroundImage = Resources.Background;
            Text = Resources.GameName;

            var userName = Environment.UserName;

            var exitButton = new Button()
            {
                Size = new Size(300, 60),
                Text = Resources.MainMenu_Exit,
                Font = new Font(Font.FontFamily, 20)
            };
            Controls.Add(exitButton);
            exitButton.Click += (sender, args) => Application.Exit();
                
            
            var startButton = new Button()
            {
                Size = new Size(300, 60),
                Text = Resources.MainMenu_Start + userName + "?",
                Font = new Font(Font.FontFamily, 20)
            };
            Controls.Add(startButton);
            startButton.Click += (sender, args) =>
            {
                Hide();
                new GameStage(Locations.CreateFirstLocation()).Show();
            };
                
            
            SizeChanged += (sender, args) =>
            {
                startButton.Location = new Point(ClientSize.Width / 2 - startButton.Size.Width / 2,
                    ClientSize.Height / 2);
                exitButton.Location = new Point(ClientSize.Width / 2 - exitButton.Size.Width / 2, 
                    startButton.Bottom + ClientSize.Height / 20 );
                BackgroundImage = GetFormBackgroundImage();
            };

            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
        }
    }
}
