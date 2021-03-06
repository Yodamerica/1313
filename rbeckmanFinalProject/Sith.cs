using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    public class Sith : Entity
    {
        public Sith(int strength, int midichlorians, int defense, int skill, int health, int speed, string name) : base(strength, midichlorians, defense, skill, health, speed, name)
        {

        }

        public bool Blocking { get => blocking; set => blocking = value; }

        /// <summary>
        /// Performs a lightsaber attack on the current target
        /// </summary>
        public void LightSaberStrike()
        {
            this.LightsaberAttack();
            ActionCooldown = ActTime;
        }

        /// <summary>
        /// Performs a force attack on each target passed into the method
        /// </summary>
        /// <param name="targets"></param>
        public void ForceChoke(List<Entity> targets)
        {
            SpecialCooldown = 100;
            ActionCooldown = ActTime;

            foreach (Entity enemy in targets)
            {
                this.Target = enemy;
                this.ForceAttack();
            }
        }
    }
}