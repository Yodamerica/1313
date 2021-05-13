using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace rbeckmanFinalProject
{
    public partial class EntityControl : UserControl
    {
        public Entity entity;
        public EventHandler EntityClicked;
        public EventHandler EntityRightClicked;

        private bool isFriendly;

        public EntityControl(Entity entity)
        {
            this.entity = entity;
            InitializeComponent();
            this.MouseClick += OnMouseClick;
            this.entityPicture.MouseClick += OnMouseClick;
            this.entityPanel.MouseClick += OnMouseClick;
            this.entityName.MouseClick += OnMouseClick;
            this.entityHealthBar.MouseClick += OnMouseClick;
            this.entityHealthPanel.MouseClick += OnMouseClick;
            this.entityHealthLabel.MouseClick += OnMouseClick;

            this.entityName.Text = entity.Name;

            this.isFriendly = this.entity.GetType() == typeof(Jedi) ||
                              this.entity.GetType() == typeof(Mandalorian) ||
                              this.entity.GetType() == typeof(Droid);

            if (IsFriendly)
            {
                this.entityHealthPanel.Dock = DockStyle.Right;
                this.entityName.Dock = DockStyle.Right;
            }
        }

        public bool IsFriendly { get => isFriendly; }
        
        /// <summary>
        /// Updates the entity's health bar (returns false if dead)
        /// </summary>
        /// <returns></returns>
        public bool UpdateHealth()
        {
            if (entity.Health <= 0)
            {
                return false;
            }
            else
            {
                entityHealthBar.Value = entity.Health;
                entityHealthLabel.Text = $"{entity.Health}";
                return true;
            }
        }

        public void HighlightOn()
        {
            if (IsFriendly) BackColor = Color.Blue;
            else BackColor = Color.Red;
        }

        public void HighlightHeal()
        {
            BackColor = Color.LimeGreen;
        }

        public void HighlightOff()
        {
            BackColor = SystemColors.Control;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && 
               (this.entity.GetType() == typeof(Jedi) || this.entity.GetType() == typeof(Mandalorian)))
            {
                EntityRightClicked(this, e);
            }
            else
            {
                EntityClicked(this, e);
            }
        }
    }
}
