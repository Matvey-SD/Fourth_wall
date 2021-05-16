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
    public partial class MainMenu : Form
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
            BackgroundImage = Resources.Background;
            FormBorderStyle = FormBorderStyle.None;
            Text = "4-th Wall";
            var rnd = new Random();

            var userName = Environment.UserName;
            var exitCounter = 0;

            var exitButton = new Button()
            {
                Size = new Size(300, 60),
                Text = Resources.MainMenu_Exit,
                Font = new Font(Font.FontFamily, 20)
            };
            Controls.Add(exitButton);
            var startButton = new Button()
            {
                Size = new Size(300, 60),
                Text = Resources.MainMenu_Start + userName + "?",
                Font = new Font(Font.FontFamily, 20)
            };
            Controls.Add(startButton);

            exitButton.Click += (sender, args) =>
                Close();
            
            SizeChanged += (sender, args) =>
            {
                startButton.Location = new Point(ClientSize.Width / 2 - startButton.Size.Width / 2,
                    ClientSize.Height / 2);
                exitButton.Location = new Point(ClientSize.Width / 2 - exitButton.Size.Width / 2, 
                    startButton.Bottom + ClientSize.Height / 20 );
                BackgroundImage = GetFormBackgroundImage();
            };

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
            
            FormClosed += (sender, args) =>
            {
                var message = MessageBox.Show(
                    Resources.Error_Text_1 + (exitCounter*10 + rnd.Next(9)).ToString() + Resources.Error_Text_2, "", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                if (exitCounter++ < 10) OnFormClosed(new FormClosedEventArgs(CloseReason.UserClosing));
            };
            
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
        }
    }
}
