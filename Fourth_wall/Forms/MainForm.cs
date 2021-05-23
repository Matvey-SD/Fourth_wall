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
            //var mainMenu = new MainMenu();
            //Controls.Add(mainMenu);
            ShowMainMenu();
            
            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show(Resources.MainMenu_On_Exit, "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
            
            /*var rnd = new Random();
            var exitCounter = 0;
            
            FormClosed += (sender, args) =>
            {
                var message = MessageBox.Show(
                    Resources.Error_Text_1 + (exitCounter*25 + rnd.Next(9)).ToString() + Resources.Error_Text_2, "", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                if (exitCounter++ < 4) OnFormClosed(new FormClosedEventArgs(CloseReason.UserClosing));
            };*/
        }

        private void HideScreens()
        {
            _mainMenu.Hide();
            _gameStage.Hide();
        }
        
        private void ShowMainMenu()
        {
            HideScreens();
            _mainMenu.Show();
        }
    }
}