using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    public class Mandalorian : Entity
    {
        public Mandalorian(int strength, int midichlorians, int defense, int skill, int health, int speed, string name) : base(strength, midichlorians, defense, skill, health, speed, name)
        {

        }

        /// <summary>
        /// Performs a blaster attack on the current target
        /// </summary>
        public void Blast()
        {
            this.BlasterAttack();
            ActionCooldown = ActTime;
        }

        /// <summary>
        /// Sets the current target on fire (or not on fire in inUse is false)
        /// </summary>
        /// <param name="inUse"></param>
        public void FlameThrower(bool inUse)
        {
            if (inUse)
            {
                FlameAttackStart();
            }
            else
            {
                FlameAttackStop();
            }
        }

        /// <summary>
        /// Performs a blaster attack with increased skill (better chance for x2 damage) on the current target
        /// </summary>
        public void Snipe()
        {
            SpecialCooldown = 100;
            ActionCooldown = ActTime;
            int temp = this.Skill;
            this.Skill = 100;
            this.BlasterAttack();
            this.Skill = temp;
        }
    }
}