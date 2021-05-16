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
        public MainMenu()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            BackgroundImage = Resources.Background;
            FormBorderStyle = FormBorderStyle.None;

            var exitButton = new Button()
            {
                Size = new Size(100, 20),
                Text = Resources.MainMenu_Exit,
            };
            Controls.Add(exitButton);

            exitButton.Click += (sender, args) =>
                Close();
            
            SizeChanged += (sender, args) =>
            {
                exitButton.Location = new Point(ClientSize.Width / 2 - exitButton.Size.Width / 2, 2*ClientSize.Height / 3 );
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
