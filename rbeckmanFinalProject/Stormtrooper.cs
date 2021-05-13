using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    public class Stormtrooper : Entity
    {
        public Stormtrooper(int strength, int midichlorians, int defense, int skill, int health, int speed) : base(strength, midichlorians, defense, skill, health, speed, "Stormtrooper")
        {

        }

        public Stormtrooper() : base(20, 2000, 10, 5, 100, 10, "Stormtrooper")
        {

        }

        /// <summary>
        /// Performs a blaster attack on the current target
        /// </summary>
        public void blast()
        {
            this.BlasterAttack();
            ActionCooldown = ActTime;
        }

        public void CallReinforcements()
        {

        }
    }
}