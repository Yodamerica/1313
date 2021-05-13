using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    public class Droid : Entity
    {
        Entity healTarget;

        public Droid(int strength, int midichlorians, int defense, int skill, int health, int speed, string name) : base(strength, midichlorians, defense, skill, health, speed, name)
        {

        }

        public bool Blocking { get => blocking; set => blocking = value; }
        public Entity HealTarget { get => healTarget; set => healTarget = value; }

        public void Heal()
        {
            SpecialCooldown = 100;
            ActionCooldown = ActTime;
            if (healTarget != null)
            {
                if (healTarget.Health <= 50)
                {
                    healTarget.Health += 50;
                }
                else
                {
                    healTarget.Health = 100;
                }
            }
        }

        public void Shock()
        {
            this.MeleeAttack();
            ActionCooldown = ActTime;
        }
    }
}