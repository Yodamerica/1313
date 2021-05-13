/* Ryan Beckman
 * CS 3020
 * Final Project 
 * 
 * 
 * Sprites designed by: Tulius Hostilius, juniorgeneral.org
 * Sprites edited by: Ryan Beckman
 * Background image credit: starwars.fandom.com/wiki/Coruscant_underworld
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace rbeckmanFinalProject
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        Random rand = new Random();
        EntityControl jedi;
        EntityControl mandalorian;
        EntityControl droid;
        EntityControl selected;
        EnemyAI empire;

        int wave;
        string statFileName = Environment.CurrentDirectory + @"/lifetimeStats.txt";
        int highScore;

        public Form1()
        {
            InitializeComponent();
            
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            Entity.filename = Environment.CurrentDirectory + @"/logs/game_log" + @time + ".log";

            using(StreamWriter log = File.CreateText(Entity.filename))
            {
                log.WriteLine("Game Log:");
            }

            if (!File.Exists(statFileName))
            {
                using (StreamWriter stats = File.CreateText(statFileName))
                {
                    stats.WriteLine("0");                                      
                }
            }

            using (StreamReader stats = File.OpenText(statFileName))
            {
                highScore = Int32.Parse(stats.ReadLine());
            }

            wave = 1;
            outputLabel.Text = $"Wave {wave}";

            highScoreLabel.Text = $"High Score: {highScore} Waves";

            SpawnMandalorian();
            SpawnJedi();
            SpawnDroid();

            empire = new EnemyAI(new List<Entity>() { jedi.entity, mandalorian.entity, droid.entity });


            SpawnStormtrooper();

            timer.Interval = 50;
            timer.Tick += OnGameTick;
            timer.Start();
            radioBtnMandalorian.CheckedChanged += radioChanged;
            radioBtnJedi.CheckedChanged += radioChanged;
            radioBtnDroid.CheckedChanged += radioChanged;

            fireBtn.Click += FireBtn_Click;
            flamethrowerBtn.MouseDown += FlamethrowerBtn_MouseDown;
            flamethrowerBtn.MouseUp += FlamethrowerBtn_MouseUp;
            snipeBtn.Click += SnipeBtn_Click;

            lightsaberStrikeBtn.Click += LightsaberStrikeBtn_Click;
            lightsaberBlockBtn.MouseDown += LightsaberBlockBtn_MouseDown;
            lightsaberBlockBtn.MouseUp += LightsaberBlockBtn_MouseUp;
            forcePushBtn.Click += ForcePushBtn_Click;

            shockBtn.Click += ShockBtn_Click;
            shieldBtn.MouseDown += ShieldBtn_MouseDown;
            shieldBtn.MouseUp += ShieldBtn_MouseUp;
            bactaBtn.Click += BactaBtn_Click;

            upBtn.Click += UpBtn_Click;
            downBtn.Click += DownBtn_Click;
            rightBtn.Click += RightBtn_Click;
            leftBtn.Click += LeftBtn_Click;

            this.KeyPreview = true;
            this.KeyPress+= Game_KeyPress;

            empire.MoveAI += EnemyMove;

            radioBtnMandalorian.Checked = false;
            radioBtnMandalorian.Checked = true;

            mandalorianActionBar.Maximum = mandalorian.entity.ActTime;
            jediActionBar.Maximum = jedi.entity.ActTime;
            droidActionBar.Maximum = droid.entity.ActTime;

        }

        /// <summary>
        /// Event handler for keypress
        /// currently controls move (wasd) and hero selection (1,2,3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
            {
                MoveUp(selected);
            }
            else if (e.KeyChar == 'a')
            {
                MoveLeft(selected);
            }
            else if (e.KeyChar == 's')
            {
                MoveDown(selected);
            }
            else if (e.KeyChar == 'd')
            {
                MoveRight(selected);
            }
            else if (e.KeyChar == '1')
            {
                radioBtnMandalorian.Checked = true;
            }
            else if (e.KeyChar == '2')
            {
                radioBtnJedi.Checked = true;
            }
            else if (e.KeyChar == '3')
            {
                radioBtnDroid.Checked = true;
            }
        }

        #region Spawn Methods
        /// <summary>
        /// Spawns a stormtrooper enemy on the battlegrid
        /// </summary>
        private void SpawnStormtrooper()
        {
            EntityControl trooper = new EntityControl(new Stormtrooper());
            trooper.EntityClicked += OnTargetChanged;

            trooper.entityPicture.Image = Properties.Resources.Stormtrooper;

            for (int row = 0; row <= 2; row++)
            {
                if (BattleGrid.GetControlFromPosition(0, row) == null)
                {
                    empire.ImperialControls.Add(trooper);
                    
                    trooper.entity.Position = new Point(0, row);
                    BattleGrid.Controls.Add(trooper, trooper.entity.Position.X, trooper.entity.Position.Y);
                    break;
                }
            }
        }

        /// <summary>
        /// Spawns a imperial security droid on the battlegrid
        /// </summary>
        private void SpawnSecurityDroid()
        {
            EntityControl kxDroid = new EntityControl(new SecurityDroid());
            kxDroid.EntityClicked += OnTargetChanged;

            kxDroid.entityPicture.Image = Properties.Resources.Security_Droid;

            for (int row = 0; row <= 2; row++)
            {
                if (BattleGrid.GetControlFromPosition(0, row) == null)
                {
                    empire.ImperialControls.Add(kxDroid);

                    kxDroid.entity.Position = new Point(0, row);
                    BattleGrid.Controls.Add(kxDroid, kxDroid.entity.Position.X, kxDroid.entity.Position.Y);
                    break;
                }
            }
        }

        /// <summary>
        /// Spawns an imperial inquisitor on the battlegrid
        /// </summary>
        private void SpawnInquisitor()
        {
            EntityControl inquisitor = new EntityControl(new Sith(30, 10000,40,50,100,50,"Third Brother"));
            inquisitor.EntityClicked += OnTargetChanged;

            inquisitor.entityPicture.Image = Properties.Resources.Inquisitor;

            for (int row = 0; row <= 2; row++)
            {
                if (BattleGrid.GetControlFromPosition(0, row) == null)
                {
                    empire.ImperialControls.Add(inquisitor);

                    inquisitor.entity.Position = new Point(0, row);
                    BattleGrid.Controls.Add(inquisitor, inquisitor.entity.Position.X, inquisitor.entity.Position.Y);
                    break;
                }
            }
        }

        /// <summary>
        /// Spawns a mandalorian hero on the battlegrid
        /// </summary>
        private void SpawnMandalorian()
        {
            mandalorian = new EntityControl(new Mandalorian(50, 2500, 90, 5, 100, 100, "Sor R'anlek"));
            mandalorian.EntityClicked += OnSelectionChanged;
            mandalorian.EntityRightClicked += OnHealTargetChanged;

            mandalorian.entityPicture.Image = Properties.Resources.Mandalorian;

            mandalorian.entity.Position = new Point(3, 0);
            BattleGrid.Controls.Add(mandalorian, mandalorian.entity.Position.X, mandalorian.entity.Position.Y);
        }

        /// <summary>
        /// Spawns a jedi hero on the battlegrid
        /// </summary>
        private void SpawnJedi()
        {
            jedi = new EntityControl(new Jedi(10, 12000, 10, 5, 100, 50, "Jedi Bob"));
            jedi.EntityClicked += OnSelectionChanged;
            jedi.EntityRightClicked += OnHealTargetChanged;

            jedi.entityPicture.Image = Properties.Resources.Jedi;

            jedi.entity.Position = new Point(2, 1);
            BattleGrid.Controls.Add(jedi, jedi.entity.Position.X, jedi.entity.Position.Y);
        }

        /// <summary>
        /// Spawns a droid hero on the battlegrid
        /// </summary>
        private void SpawnDroid()
        {
            droid = new EntityControl(new Droid(20, 0, 40, 100, 100, 50, "BH-18"));
            droid.EntityClicked += OnSelectionChanged;

            droid.entityPicture.Image = Properties.Resources.Droid;

            droid.entity.Position = new Point(3, 2);
            BattleGrid.Controls.Add(droid, droid.entity.Position.X, droid.entity.Position.Y);
        }
        #endregion

        /// <summary>
        /// Event handler for each game tick determined by the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGameTick(object sender, EventArgs e)
        {
            foreach (EntityControl entityControl in BattleGrid.Controls)
            {
                entityControl.entity.Tick();

                //reset action pictures
                if (entityControl.entity.ActionCooldown == 1)
                {
                    if (entityControl.entity.GetType() == typeof(Mandalorian))
                    {
                        entityControl.entityPicture.Image = Properties.Resources.Mandalorian;
                    }
                    else if (entityControl.entity.GetType() == typeof(Droid))
                    {
                        entityControl.entityPicture.Image = Properties.Resources.Droid;
                    }
                    else if (entityControl.entity.GetType() == typeof(Stormtrooper))
                    {
                        entityControl.entityPicture.Image = Properties.Resources.Stormtrooper;
                    }
                    else if (entityControl.entity.GetType() == typeof(Sith))
                    {
                        ((Sith)entityControl.entity).Blocking = false;
                        entityControl.entityPicture.Image = Properties.Resources.Inquisitor;
                    }
                }

                //update health and remove dead entities
                if (!entityControl.UpdateHealth())
                {
                    //check for game over
                    if (this.empire.Rebels.Contains(entityControl.entity))
                    {
                        using (StreamWriter log = File.AppendText(Entity.filename))
                        {
                            log.WriteLine($"\nGame Over! A member of your crew died.\n");
                        }
                        outputLabel.Text = "Game Over! A member of your crew died.";
                        outputLabel.Refresh();
                        rightBtn.Enabled = false;
                        leftBtn.Enabled = false;
                        upBtn.Enabled = false;
                        downBtn.Enabled = false;
                        System.Threading.Thread.Sleep(10000);
                        wave = 1;
                        outputLabel.Text = $"Wave {wave}";
                        outputLabel.Refresh();

                        foreach (EntityControl enemy in empire.ImperialControls)
                        {
                            BattleGrid.Controls.Remove(enemy);
                        }
                        empire.ImperialControls = new List<EntityControl>();

                        BattleGrid.Controls.Remove(mandalorian);
                        BattleGrid.Controls.Remove(jedi);
                        BattleGrid.Controls.Remove(droid);
                        SpawnMandalorian();
                        SpawnJedi();
                        SpawnDroid();
                        SpawnStormtrooper();
                        empire.Rebels = new List<Entity>() { jedi.entity, mandalorian.entity, droid.entity };
                        rightBtn.Enabled = true;
                        leftBtn.Enabled = true;
                        upBtn.Enabled = true;
                        downBtn.Enabled = true;
                    }
                    else
                    {
                        this.BattleGrid.Controls.Remove(entityControl);
                        this.empire.ImperialControls.Remove(entityControl);
                    }                    
                }
            }

            //update progress bars
            mandalorianSpecialBar.Value = 100 - mandalorian.entity.SpecialCooldown;
            jediSpecialBar.Value = 100 - jedi.entity.SpecialCooldown;
            droidSpecialBar.Value = 100 - droid.entity.SpecialCooldown;

            if (mandalorianSpecialBar.Value != 100) snipeBtn.Enabled = false;
            if (jediSpecialBar.Value != 100) forcePushBtn.Enabled = false;
            if (droidSpecialBar.Value != 100) bactaBtn.Enabled = false;

            mandalorianActionBar.Value = mandalorian.entity.ActTime - mandalorian.entity.ActionCooldown;
            jediActionBar.Value = jedi.entity.ActTime - jedi.entity.ActionCooldown;
            droidActionBar.Value = droid.entity.ActTime - droid.entity.ActionCooldown;

            //enable and disable mandalorian buttons
            if (mandalorianActionBar.Value != mandalorian.entity.ActTime)
            {
                fireBtn.Enabled = false;
                flamethrowerBtn.Enabled = false;
                snipeBtn.Enabled = false;
            }
            else
            {
                fireBtn.Enabled = true;
                flamethrowerBtn.Enabled = true;
                if (mandalorianSpecialBar.Value == 100)
                {
                    snipeBtn.Enabled = true;
                }
            }

            //enable and disable jedi buttons
            if (jediActionBar.Value != jedi.entity.ActTime)
            {
                lightsaberStrikeBtn.Enabled = false;
                lightsaberBlockBtn.Enabled = false;
                forcePushBtn.Enabled = false;
            }
            else
            {
                lightsaberStrikeBtn.Enabled = true;
                lightsaberBlockBtn.Enabled = true;
                if (jediSpecialBar.Value == 100)
                {
                    forcePushBtn.Enabled = true;
                }
            }

            //enable and disable droid buttons
            if (droidActionBar.Value != droid.entity.ActTime)
            {
                shockBtn.Enabled = false;
                shieldBtn.Enabled = false;
                bactaBtn.Enabled = false;
            }
            else
            {
                shockBtn.Enabled = true;
                shieldBtn.Enabled = true;
                if (droidSpecialBar.Value == 100)
                {
                    bactaBtn.Enabled = true;
                }
            }

            empire.EnemyAct();

            //wave over code
            if (empire.ImperialEntities.Count == 0)
            {
                droid.entity.Health = 100;

                using (StreamWriter log = File.AppendText(Entity.filename))
                {
                    log.WriteLine($"\nWave {wave} complete! Your droid has been repaired\n");
                }

                if (wave > highScore)
                {
                    highScore = wave;
                    using (StreamWriter stats = File.CreateText(statFileName))
                    {
                        stats.WriteLine(highScore);
                    }
                    highScoreLabel.Text = highScoreLabel.Text = $"High Score: {highScore} Waves";
                }

                wave++;

                outputLabel.Text = $"Wave {wave}";
                outputLabel.Refresh();
                highScoreLabel.Refresh();

                if (wave % 10 == 0)
                {
                    SpawnInquisitor();
                }
                else
                {
                    for (int i = 0; i < rand.Next(1, 4); i++)
                    {
                        if (rand.Next(0, 2) == 0)
                        {
                            SpawnStormtrooper();
                        }
                        else
                        {
                            SpawnSecurityDroid();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for when a the curren hero selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            Entity entity = ((EntityControl)sender).entity;

            if (entity.GetType() == typeof(Jedi)) radioBtnJedi.Checked = true;
            else if (entity.GetType() == typeof(Mandalorian)) radioBtnMandalorian.Checked = true;
            else if (entity.GetType() == typeof(Droid)) radioBtnDroid.Checked = true;

        }

        /// <summary>
        /// Event handler for refreshing the red target highlight when a new enemy is selected as the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTargetChanged(object sender, EventArgs e)
        {
            EntityControl temp = (EntityControl)sender;
            foreach (EntityControl enemy in empire.ImperialControls)
            {
                enemy.HighlightOff();
            }

            selected.entity.Target = temp.entity;
            temp.HighlightOn();
        }

        /// <summary>
        /// Event handler for refreshing the green heal target highlight when a hero is slected as the new heal target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHealTargetChanged(object sender, EventArgs e)
        {
            if (radioBtnDroid.Checked)
            {
                mandalorian.HighlightOff();
                jedi.HighlightOff();

                ((Droid)droid.entity).HealTarget = ((EntityControl)sender).entity;
                ((EntityControl)sender).HighlightHeal();
            }
        }

        /// <summary>
        /// Event handler for when the hero radio button changes (updates blue selection highlight)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;

            switch (temp.TabIndex)
            {
                case 0:
                    selected = mandalorian;
                    mandalorian.HighlightOn();
                    jedi.HighlightOff();
                    droid.HighlightOff();
                    break;
                case 1:
                    selected = jedi;
                    mandalorian.HighlightOff();
                    jedi.HighlightOn();
                    droid.HighlightOff();
                    break;
                case 2:
                    selected = droid;
                    mandalorian.HighlightOff();
                    jedi.HighlightOff();
                    droid.HighlightOn();
                    break;
                default:
                    break;
            }

            UpdateTarget();
            UpdateHealTarget();

        }

        /// <summary>
        /// Updates the red highlight to the selected hero's target
        /// </summary>
        private void UpdateTarget()
        {
            foreach (EntityControl entityControl in empire.ImperialControls)
            {
                entityControl.HighlightOff();

                if (selected.entity.Target == entityControl.entity)
                {
                    entityControl.HighlightOn();
                }
            }
        }

        /// <summary>
        /// Updates the green heal target highlight if the droid is selected
        /// </summary>
        private void UpdateHealTarget()
        {
            if (radioBtnDroid.Checked)
            {
                if (((Droid)droid.entity).HealTarget == mandalorian.entity)
                {
                    mandalorian.HighlightHeal();
                }
                else if (((Droid)droid.entity).HealTarget == jedi.entity)
                {
                    jedi.HighlightHeal();
                }
            }
        }

        #region Action Button Event Handlers
        /// <summary>
        /// Event handler for mandalorian fire blaster button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FireBtn_Click(object sender, EventArgs e)
        {
            radioBtnMandalorian.Checked = true;
            mandalorian.entityPicture.Image = Properties.Resources.Mandalorian_Fire;
            ((Mandalorian)mandalorian.entity).Blast();
        }

        /// <summary>
        /// Event handler for jedi lightsaber strike button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsaberStrikeBtn_Click(object sender, EventArgs e)
        {
            radioBtnJedi.Checked = true;
            jedi.entity.SetMeleeTarget(empire.ImperialEntities);
            UpdateTarget();
            ((Jedi)jedi.entity).LightsaberStrike();
        }

        /// <summary>
        /// Event handler for droid shock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShockBtn_Click(object sender, EventArgs e)
        {
            radioBtnDroid.Checked = true;
            droid.entity.SetMeleeTarget(empire.ImperialEntities);
            UpdateTarget();
            droid.entityPicture.Image = Properties.Resources.Droid_Shock;
            ((Droid)droid.entity).Shock();
        }

        /// <summary>
        /// Event handler for mandalorian flamethrower button down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlamethrowerBtn_MouseDown(object sender, EventArgs e)
        {
            radioBtnMandalorian.Checked = true;
            mandalorian.entity.SetMeleeTarget(empire.ImperialEntities);
            UpdateTarget();
            mandalorian.entityPicture.Image = Properties.Resources.Mandalorian_Flamethrower;
            ((Mandalorian)mandalorian.entity).FlameThrower(true);
        }

        /// <summary>
        /// Event handler for mandalorian flamethrower button up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlamethrowerBtn_MouseUp(object sender, EventArgs e)
        {
            mandalorian.entityPicture.Image = Properties.Resources.Mandalorian;
            ((Mandalorian)mandalorian.entity).FlameThrower(false);
        }

        /// <summary>
        /// Event handler for jedi lightsaber block button down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsaberBlockBtn_MouseDown(object sender, EventArgs e)
        {
            radioBtnJedi.Checked = true;
            ((Jedi)jedi.entity).Blocking = true;
        }

        /// <summary>
        /// Event handler for jedi lightsaber block button up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsaberBlockBtn_MouseUp(object sender, EventArgs e)
        {
            ((Jedi)jedi.entity).Blocking = false;
        }

        /// <summary>
        /// Event handler for droid shield button down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShieldBtn_MouseDown(object sender, EventArgs e)
        {
            radioBtnDroid.Checked = true;
            ((Droid)droid.entity).Blocking = true;
        }

        /// <summary>
        /// Event handler for droid shield button up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShieldBtn_MouseUp(object sender, EventArgs e)
        {
            ((Droid)droid.entity).Blocking = false;
        }

        /// <summary>
        /// Event handler for mandalorian snipe button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnipeBtn_Click(object sender, EventArgs e)
        {
            radioBtnMandalorian.Checked = true;
            mandalorian.entityPicture.Image = Properties.Resources.Mandalorian_Fire;
            ((Mandalorian)mandalorian.entity).Snipe();
        }

        /// <summary>
        /// Event handler for jedi force push button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForcePushBtn_Click(object sender, EventArgs e)
        {
            radioBtnJedi.Checked = true;
            ((Jedi)jedi.entity).ForcePush(empire.ImperialEntities);
        }

        /// <summary>
        /// Event handler for droid bacta button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BactaBtn_Click(object sender, EventArgs e)
        {
            radioBtnDroid.Checked = true;
            ((Droid)droid.entity).Heal();
        }
        #endregion

        #region Movement Methods and Handlers
        /// <summary>
        /// Event handler for the player's left button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftBtn_Click(object sender, EventArgs e)
        {
            MoveLeft(selected);
        }

        /// <summary>
        /// Attempts to move the given EntityControl left one space
        /// </summary>
        /// <param name="movingEntity"></param>
        private void MoveLeft(EntityControl movingEntity)
        {
            Point current = movingEntity.entity.Position;

            if (current.X - 1 >= 0)
            {
                if (BattleGrid.GetControlFromPosition(current.X - 1, current.Y) == null)
                {
                    movingEntity.entity.Position = new Point(current.X - 1, current.Y);
                    BattleGrid.Controls.Remove(movingEntity);
                    BattleGrid.Controls.Add(movingEntity, movingEntity.entity.Position.X, movingEntity.entity.Position.Y);
                }
            }
        }

        /// <summary>
        /// Event handler for the player's right button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightBtn_Click(object sender, EventArgs e)
        {
            MoveRight(selected);
        }

        /// <summary>
        /// Attempts to move the given EntityControl right one space
        /// </summary>
        /// <param name="movingEntity"></param>
        private void MoveRight(EntityControl movingEntity)
        {
            Point current = movingEntity.entity.Position;

            if (current.X + 1 <= 3)
            {
                if (BattleGrid.GetControlFromPosition(current.X + 1, current.Y) == null)
                {
                    movingEntity.entity.Position = new Point(current.X + 1, current.Y);
                    BattleGrid.Controls.Remove(movingEntity);
                    BattleGrid.Controls.Add(movingEntity, movingEntity.entity.Position.X, movingEntity.entity.Position.Y);
                }
            }
        }

        /// <summary>
        /// Event handler for the player's down button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownBtn_Click(object sender, EventArgs e)
        {
            MoveDown(selected);
        }

        /// <summary>
        /// Attempts to move the given EntityControl down one space
        /// </summary>
        /// <param name="movingEntity"></param>
        private void MoveDown(EntityControl movingEntity)
        {
            Point current = movingEntity.entity.Position;

            if (current.Y + 1 <= 3)
            {
                if (BattleGrid.GetControlFromPosition(current.X, current.Y + 1) == null)
                {
                    movingEntity.entity.Position = new Point(current.X, current.Y + 1);
                    BattleGrid.Controls.Remove(movingEntity);
                    BattleGrid.Controls.Add(movingEntity, movingEntity.entity.Position.X, movingEntity.entity.Position.Y);
                }
            }
        }

        /// <summary>
        /// Event handler for the player's up button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpBtn_Click(object sender, EventArgs e)
        {
            MoveUp(selected);
        }

        /// <summary>
        /// Attempts to move the given EntityControl up one space
        /// </summary>
        /// <param name="movingEntity"></param>
        private void MoveUp(EntityControl movingEntity)
        {
            Point current = movingEntity.entity.Position;

            if (current.Y - 1 >= 0)
            {
                if (BattleGrid.GetControlFromPosition(current.X, current.Y - 1) == null)
                {
                    movingEntity.entity.Position = new Point(current.X, current.Y - 1);
                    BattleGrid.Controls.Remove(movingEntity);
                    BattleGrid.Controls.Add(movingEntity, movingEntity.entity.Position.X, movingEntity.entity.Position.Y);
                }
            }
        }

        /// <summary>
        /// Event handler for when an AI enemy move event is raised
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyMove(object sender, EventArgs e)
        {
            EntityControl enemy = ((EntityControl)sender);

            List<Point> attackPositions = new List<Point>();
            foreach (Entity rebel in empire.Rebels)
            {
                attackPositions.Add(new Point(rebel.Position.X - 1, rebel.Position.Y));
            }

            if (enemy.entity.GetType() == typeof(Stormtrooper))
            {
                if (enemy.entity.Position.X == 0)
                {
                    MoveRight(enemy);
                    using (StreamWriter log = File.AppendText(Entity.filename))
                    {
                        log.WriteLine($"{enemy.entity.Name} moving to X: {enemy.entity.Position.X} Y: {enemy.entity.Position.Y}");
                    }
                }
            }
            else if (enemy.entity.GetType() == typeof(SecurityDroid))
            {
                if (((MoveEventArgs)e).Retreat)
                {
                    MoveLeft(enemy);
                    using (StreamWriter log = File.AppendText(Entity.filename))
                    {
                        log.WriteLine($"{enemy.entity.Name} moving to X: {enemy.entity.Position.X} Y: {enemy.entity.Position.Y}");
                    }
                }
                else
                {
                    PathfindMelee(enemy, attackPositions);
                }
            }
            else if (enemy.entity.GetType() == typeof(Sith))
            {
                PathfindMelee(enemy, attackPositions);
            }
        }

        /// <summary>
        /// Pathfinds and moves a given EntityControl to the closest space in the list attackPositions
        /// Uses modified A* algorithm
        /// </summary>
        /// <param name="enemy"></param>
        private void PathfindMelee(EntityControl enemy, List<Point> attackPositions)
        {
            Queue<int[]> path = new Queue<int[]>();
            Queue<int[]> pathClone = new Queue<int[]>();
            List<Point> moves = new List<Point>();

            path.Enqueue(new int[] { enemy.entity.Position.X, enemy.entity.Position.Y, 0 });
            pathClone.Enqueue(path.Peek());

            bool targetFound = false;

            while (!targetFound && pathClone.Count > 0)
            {
                int[] coord = pathClone.Dequeue();

                List<int[]> testCoords = new List<int[]>();                
                testCoords.Add(new int[] { coord[0], coord[1] + 1, coord[2] + 1 });
                testCoords.Add(new int[] { coord[0], coord[1] - 1, coord[2] + 1 });
                testCoords.Add(new int[] { coord[0] + 1, coord[1], coord[2] + 1 });
                testCoords.Add(new int[] { coord[0] - 1, coord[1], coord[2] + 1 });

                List<int[]> testCoordsClone = testCoords.ToList();

                for (int i = 0; i < testCoordsClone.Count(); i++)
                {
                    int[] temp = testCoordsClone[i];

                    if (temp[0] < 0 || temp[0] > 3 || temp[1] < 0 || temp[1] > 2)
                    {
                        testCoords.Remove(temp);
                    }
                    else if (BattleGrid.GetControlFromPosition(temp[0], temp[1]) != null)
                    {
                        testCoords.Remove(temp);
                    }

                    var duplicate = from s in path
                                    where s[0] == temp[0]
                                    where s[1] == temp[1]
                                    select s;

                    if (duplicate.ToList().Count() != 0)
                    {
                        testCoords.Remove(temp);
                    }
                }

                foreach (int[] testCoord in testCoords)
                {
                    Point testPoint = new Point(testCoord[0], testCoord[1]);

                    if (attackPositions.Contains(testPoint) && !targetFound)
                    {
                        using (StreamWriter log = File.AppendText(Entity.filename))
                        {
                            log.WriteLine($"{enemy.entity.Name} moving to X: {testPoint.X} Y: {testPoint.Y}");
                        }
                        targetFound = true;
                        moves.Add(testPoint);
                    }
                    else if (!targetFound)
                    {
                        path.Enqueue(testCoord);
                        pathClone.Enqueue(testCoord);
                    }                        
                }
            }

            if (moves.Count > 0)
            {
                while (moves[moves.Count - 1] != enemy.entity.Position)
                {
                    Point current = moves[moves.Count - 1];

                    var surPts = from coord in path
                                 where (coord[0] == current.X && (coord[1] + 1 == current.Y || coord[1] - 1 == current.Y)) ||
                                       (coord[1] == current.Y && (coord[0] + 1 == current.X || coord[0] - 1 == current.X))
                                 select coord;

                    int lowest = surPts.Min(x => x[2]);

                    int[] nextPos = surPts.Where(x => x[2] == lowest).First();

                    moves.Add(new Point(nextPos[0], nextPos[1]));
                }
            }

            moves.Reverse();

            for (int i = 1; i < moves.Count; i++)
            {
                MoveByCoords(moves[i - 1], moves[i], enemy);
            }
        }

        /// <summary>
        /// Moves a given EntityControl from the current point to the destination point
        /// Should only be used to travel a single space in one of the four cardinal directions
        /// </summary>
        /// <param name="current"></param>
        /// <param name="destination"></param>
        /// <param name="enemy"></param>
        private void MoveByCoords(Point current, Point destination, EntityControl enemy)
        {
            if (current.X == destination.X)
            {
                if (current.Y > destination.Y)
                {
                    MoveUp(enemy);
                }
                else
                {
                    MoveDown(enemy);
                }
            }
            else if (current.X > destination.X)
            {
                MoveLeft(enemy);
            }
            else
            {
                MoveRight(enemy);
            }
        }

        #endregion
    }
}