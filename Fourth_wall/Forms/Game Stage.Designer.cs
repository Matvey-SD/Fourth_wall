﻿using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Collections.Generic;

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
            
            BackgroundImage = Image.FromFile(@"..\..\Resources\Floor.png");
            
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight0.png"));
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight1.png"));
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight2.png"));
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight3.png"));
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight4.png"));
            _heroRunRightImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunRight5.png"));
            _heroStandRightImage = Image.FromFile(@"..\..\Resources\Hero\HeroStandRight.png");
            _heroAttackRightImage = Image.FromFile(@"..\..\Resources\Hero\HeroAttackRight.png");
            
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft0.png"));
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft1.png"));
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft2.png"));
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft3.png"));
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft4.png"));
            _heroRunLeftImages.Add(Image.FromFile(@"..\..\Resources\Hero\HeroRunLeft5.png"));
            _heroStandLeftImage = Image.FromFile(@"..\..\Resources\Hero\HeroStandLeft.png");
            _heroAttackLeftImage = Image.FromFile(@"..\..\Resources\Hero\HeroAttackLeft.png");
            
            _enemyImage = Image.FromFile(@"..\..\Resources\Enemy\Enemy.png");
            _deadEnemy = Image.FromFile(@"..\..\Resources\Enemy\DeadEnemy.png");
            
            _wallImage = Image.FromFile(@"..\..\Resources\Wall.png");
            
            _boxImage = Image.FromFile(@"..\..\Resources\Box\Box.png");
            _damagedBoxImage = Image.FromFile(@"..\..\Resources\Box\BoxDamaged.png");
            _brokenBoxImage = Image.FromFile(@"..\..\Resources\Box\BoxBroken.png");
            
            _chestClosedImage = Image.FromFile(@"..\..\Resources\Chest\ChestClosed.png");
            _chestOpenedImage = Image.FromFile(@"..\..\Resources\Chest\ChestOpened.png");
            
            _holeImage = Image.FromFile(@"..\..\Resources\Hole\Hole.png");
            
            _hitSound = new SoundPlayer(@"..\..\Resources\Sounds\Hit.wav");
            _explosionSound = new SoundPlayer(@"..\..\Resources\Sounds\Explosion.wav");
            _deathSound = new SoundPlayer(@"..\..\Resources\Sounds\Death.wav");
            _chestSound = new SoundPlayer(@"..\..\Resources\Sounds\Chest.wav");
            _damageSound = new SoundPlayer(@"..\..\Resources\Sounds\Damage.wav");
            _monsterDeathSound = new SoundPlayer(@"..\..\Resources\Sounds\MonsterDeath.wav");
            _boxDestructionSound = new SoundPlayer(@"..\..\Resources\Sounds\BoxDestruction.wav");
        }

        #endregion

        private Image _heroStandRightImage;
        private List<Image> _heroRunRightImages = new List<Image>();
        private Image _heroAttackRightImage;
        private Image _heroStandLeftImage;
        private List<Image> _heroRunLeftImages = new List<Image>();
        private Image _heroAttackLeftImage;
        
        private Image _enemyImage;
        private Image _deadEnemy;
        
        private Image _wallImage;
        
        private Image _boxImage;
        private Image _damagedBoxImage;
        private Image _brokenBoxImage;
        
        private Image _chestClosedImage;
        private Image _chestOpenedImage;
        
        private Image _holeImage;

        private SoundPlayer _hitSound;
        private SoundPlayer _explosionSound;
        private SoundPlayer _deathSound;
        private SoundPlayer _chestSound;
        private SoundPlayer _damageSound;
        private SoundPlayer _monsterDeathSound;
        private SoundPlayer _boxDestructionSound;
    }
}