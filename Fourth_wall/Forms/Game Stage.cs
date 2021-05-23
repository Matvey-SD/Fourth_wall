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
                    location.TryMoveHero(Directions.Up);
                    break;
                case 's' :
                    location.TryMoveHero(Directions.Down);
                    break;
                case 'd' :
                    location.TryMoveHero(Directions.Right);   
                    break;
                case 'a' :
                    location.TryMoveHero(Directions.Left);
                    break;
            }
        }
    }
}