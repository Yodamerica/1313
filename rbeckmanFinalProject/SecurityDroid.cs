using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    public class SecurityDroid : Entity
    {
        public SecurityDroid(int strength, int midichlorians, int defense, int skill, int health, int speed, string name) : base(strength, midichlorians, defense, skill, health, speed, name)
        {

        }

        public SecurityDroid() : base(100, 0, 90, 100, 100, 10, "Security Droid")
        {

        }

        public void Punch()
        {
            this.MeleeAttack();
            ActionCooldown = ActTime;
        }

        public void GroundSlam()
        {
            SpecialCooldown = 100;
            ActionCooldown = ActTime;

            this.MeleeAttack();
            if (Target != null) Target.ActionCooldown = Target.ActTime;
            this.KnockDownAttack();
        }

        public void Explode()
        {

        }
    }
}