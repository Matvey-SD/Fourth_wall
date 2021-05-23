using System;
using System.Windows.Forms;
using Fourth_wall.Properties;

namespace Fourth_wall
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            var mainMenu = new MainMenu();
            Controls.Add(mainMenu);
            ShowMainMenu();

            var rnd = new Random();
            var exitCounter = 0;
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
        }

        private void HideScreens()
        {
            _mainMenu.Hide();
        }
        
        private void ShowMainMenu()
        {
            HideScreens();
            _mainMenu.Show();
        }
    }
}