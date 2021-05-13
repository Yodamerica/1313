using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace rbeckmanFinalProject
{
    public abstract class Entity
    {
        static Random rand = new Random();
        public static string filename;
        
        //internal vars (not accessed by external game)
        private int strength;
        private int midichlorians;
        private int defense;
        private int damage;
        private bool onFire;

        //vars accessed externally through properties
        private int health;
        private int speed;
        private int actTime;
        private int skill;
        private int specialCooldown;
        private int actionCooldown;
        protected bool blocking;
        private string name;

        private Entity target;
        private Point position;

        public Entity(int strength, int midichlorians, int defense, int skill, int health, int speed, string name)
        {
            this.strength = strength;
            this.midichlorians = midichlorians;
            this.defense = defense;
            this.health = health;
            this.speed = speed;
            this.name = name;
            this.blocking = false;
            this.skill = skill;
            this.onFire = false;
            this.specialCooldown = 0;
            this.actionCooldown = 0;
            this.actTime = 50 - (3 * speed / 8);
        }

        #region Properties
        public int Health { get => health; set => health = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Skill
        {
            get => skill;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    skill = value;
                }
            }
        }
        public int SpecialCooldown
        {
            get => specialCooldown;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    specialCooldown = value;
                }
            }
        }
        public string Name { get => name; set => name = value; }
        public Entity Target { get => target; set => target = value; }
        public Point Position
        {
            get => position;
            set
            {
                if (value.X >= 0 && value.X < 4 && value.Y >= 0 && value.Y < 3)
                {
                    position = value;
                }
            }

        }
        public int ActionCooldown
        {
            get => actionCooldown;
            set
            {
                if (value >= 0 && value <= actTime)
                {
                    actionCooldown = value;
                }
            }
        }
        public int ActTime { get => actTime; }
        public bool OnFire { get => onFire; set => onFire = value; }
        #endregion

        /// <summary>
        /// Melee attacks current target
        /// </summary>
        protected void MeleeAttack()
        {
            if (target != null)
            {
                this.damage = this.strength / 2;
                target.health -= this.damage * (100 - target.defense) / 100;

                using (StreamWriter log = File.AppendText(filename))
                {
                    log.WriteLine($"{ this.name } melee hit { this.target.name } dealing { this.damage * (100 - target.defense) / 100 } damage.");
                }

                if (target.health <= 0) target = null;
            } 
        }

        /// <summary>
        /// Knock down attacks current target
        /// </summary>
        protected void KnockDownAttack()
        {
            if (target != null)
            {
                this.damage = this.strength / 4;
                target.health -= this.damage;
                using (StreamWriter log = File.AppendText(filename))
                {
                    log.WriteLine($"{ this.name } knocked down { this.target.name } dealing { this.damage } damage.");
                }

                if (target.health <= 0) target = null;
            }
        }

        /// <summary>
        /// Lightsaber attacks current target
        /// </summary>
        protected void LightsaberAttack()
        {
            if (target != null)
            {             
                if (!target.blocking)
                {
                    this.damage = (-1000 / ((this.midichlorians + 30000) * (this.midichlorians + 30000) / 100000000)) + 100;

                    //check for critical hit (bypass armor)
                    if (this.skill > rand.Next(0, 400))
                    {
                        target.health -= this.damage;
                        using (StreamWriter log = File.AppendText(filename))
                        {
                            log.WriteLine($"{ this.name } slashed past { this.target.name }'s armor with lightsaber dealing { this.damage } damage.");
                        }
                    }
                    else
                    {
                        target.health -= damage * (100 - (target.defense * target.midichlorians / (target.midichlorians + 1))) / 100;
                        using (StreamWriter log = File.AppendText(filename))
                        {
                            log.WriteLine($"{ this.name } slashed { this.target.name } with lightsaber dealing { damage * (100 - (target.defense * target.midichlorians / (target.midichlorians + 1))) / 100 } damage.");
                        }
                    }
                }
                else if (target.GetType() == typeof(Droid))
                {
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.target.name } used shield to block lightsaber attack by {this.name} damage.");
                    }
                }
                else
                {
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.target.name } used lightsaber to block lightsaber attack by {this.name} damage.");
                    }
                }

                if (target.health <= 0) target = null;
            } 
        }

        /// <summary>
        /// Blaster attacks current target
        /// </summary>
        protected void BlasterAttack()
        {
            if (target != null)
            {
                if (!target.blocking)
                {
                    this.damage = (9 * this.strength / 10) + ((this.midichlorians - 2000) / 100);

                    //check for headshot bypas (x2 damage)
                    if (this.skill > rand.Next(0, 400))
                    {
                        this.damage = this.damage * 2;
                        target.health -= this.damage * (100 - target.defense) / 100;
                        using (StreamWriter log = File.AppendText(filename))
                        {
                            log.WriteLine($"{ this.name } shot { this.target.name } with a blaster in the head dealing { this.damage * (100 - target.defense) / 100 } damage.");
                        }
                    }
                    else
                    {
                        target.health -= this.damage * (100 - target.defense) / 100;
                        using (StreamWriter log = File.AppendText(filename))
                        {
                            log.WriteLine($"{ this.name } shot { this.target.name } with a blaster dealing { this.damage * (100 - target.defense) / 100 } damage.");
                        }
                    }
                }
                else if (target.GetType() == typeof(Droid))
                {
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.target.name } used shield to block blaster attack by {this.name} damage.");
                    }
                }
                else
                {
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.target.name } used lightsaber to block blaster attack by {this.name} damage.");
                    }
                }
                
                if (target.health <= 0) target = null;
            } 
        }

        /// <summary>
        /// Force attacks current target
        /// </summary>
        protected void ForceAttack()
        {
            if (target != null)
            {
                if (!(target.blocking && target.GetType() == typeof(Droid)))
                {
                    this.damage = this.midichlorians / 250;

                    target.health -= this.damage * ((target.midichlorians / 100) - 300) * ((target.midichlorians / 100) - 300) / 90000;
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.name } used the force to attack { this.target.name } dealing { this.damage * ((target.midichlorians / 100) - 300) * ((target.midichlorians / 100) - 300) / 90000 } damage.");
                    }
                }
                else
                {
                    using (StreamWriter log = File.AppendText(filename))
                    {
                        log.WriteLine($"{ this.target.name } used shield to block force attack by {this.name} damage.");
                    }
                }
            }
        }

        /// <summary>
        /// Flame attacks current target
        /// </summary>
        protected void FlameAttackStart()
        {
            if (target != null)
            {
                if (target.midichlorians > 0)
                {
                    target.onFire = true;
                    target.actionCooldown = 1;
                }
            }
        }

        /// <summary>
        /// Ends flame attack
        /// </summary>
        protected void FlameAttackStop()
        {
            if (target != null)
            {
                target.onFire = false;
            }
        }

        /// <summary>
        /// Updates cooldowns and fire damage
        /// Intended to be called every game tick
        /// </summary>
        public void Tick()
        {
            if (specialCooldown > 0)
            {
                specialCooldown--;
            }
            if (actionCooldown > 0 && !onFire)
            {
                actionCooldown--;
            }
            if (onFire)
            {
                health--;
                using (StreamWriter log = File.AppendText(filename))
                {
                    log.WriteLine($"{ this.name } took 1 damage from fire.");
                }   
            }
        }

        /// <summary>
        /// Sets the current target any adjacent entity on the opposite team to the right or left
        /// </summary>
        /// <param name="enemies"></param>
        public void SetMeleeTarget(List<Entity> enemies)
        {
            target = null;

            foreach (Entity enemy in enemies)
            {
                if (Math.Abs(enemy.position.X - this.position.X) == 1 && enemy.position.Y == this.position.Y)
                {
                    this.target = enemy;
                }
            }
        }
    }
}
