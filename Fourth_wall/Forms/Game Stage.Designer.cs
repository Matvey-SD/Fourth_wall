using System.ComponentModel;
using System.Drawing;

namespace Fourth_wall
{
    sealed partial class GameStage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            BackgroundImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Floor.png");
            _heroImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hero.png");
            _heroAttackImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\HeroAttack.png");
            _heroLeftImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\HeroLeft.png");
            _heroAttackLeftImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\HeroAttackLeft.png");
            _enemyImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Enemy.png");
            _deadEnemy = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\DeadEnemy.png");
            _wallImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Wall.png");
            _boxImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Box.png");
            _brokenBoxImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\BoxBroken.png");
            _chestClosedImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\ChestClosed.png");
            _chestOpenedImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\ChestOpened.png");
            _holeImage = Image.FromFile("C:\\Users\\Матвей\\Desktop\\Projects\\Fourth_wall\\Fourth_wall\\Resources\\Hole.png");
        }

        #endregion
        
        private Image _heroImage;
        private Image _heroAttackImage;
        private Image _heroLeftImage;
        private Image _heroAttackLeftImage;
        private Image _enemyImage;
        private Image _deadEnemy;
        private Image _wallImage;
        private Image _boxImage;
        private Image _brokenBoxImage;
        private Image _chestClosedImage;
        private Image _chestOpenedImage;
        private Image _holeImage;
    }
}