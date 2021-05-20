using System.Windows.Forms;

namespace Fourth_wall
{
    public partial class GameStage : UserControl
    {
        private Location location;
        
        public GameStage(Location location)
        {
            this.location = location;
            InitializeComponent();
            KeyPress += GameStage_KeyPress;
        }

        private void GameStage_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w' :
                    location.Hero.ChangeLocation(0, -2);
                    break;
                case 's' :
                    location.Hero.ChangeLocation(0, 2);
                    break;
                case 'd' :
                    location.Hero.ChangeLocation(2, 0);
                    break;
                case 'a' :
                    location.Hero.ChangeLocation(-2, 0);
                    break;
            }
        }
    }
}