using System.Windows.Forms;

namespace Fourth_wall
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            ShowMainMenu();
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