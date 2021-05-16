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
            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(this.BackgroundImage,
                    new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            return bmp;
        }
        public MainMenu()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Resources.Background;
            FormBorderStyle = FormBorderStyle.None;
            Text = "4-th Wall";

            var exitButton = new Button()
            {
                Size = new Size(300, 60),
                Text = Resources.MainMenu_Exit,
            };
            Controls.Add(exitButton);

            exitButton.Click += (sender, args) =>
                Close();
            
            SizeChanged += (sender, args) =>
            {
                exitButton.Location = new Point(ClientSize.Width / 2 - exitButton.Size.Width / 2, 
                    2*ClientSize.Height / 3 );
                BackgroundImage = GetFormBackgroundImage();
            };

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
            
            Load += (sender, args) => OnSizeChanged(EventArgs.Empty);
        }
    }
}
