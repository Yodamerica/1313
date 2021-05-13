using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    /// <summary>
    /// Class controls enemies making them perform actions in the game
    /// </summary>
    class EnemyAI
    {
        public EventHandler MoveAI;

        Random AIrand = new Random();
        List<EntityControl> imperialControls = new List<EntityControl>();
        List<Entity> imperialEntities = new List<Entity>();
        List<Entity> rebels = new List<Entity>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rebels"></param>
        public EnemyAI(List<Entity> rebels)
        {
            this.rebels = rebels;
        }

        #region Properties
        public List<EntityControl> ImperialControls { get => imperialControls; set => imperialControls = value; }
        public List<Entity> ImperialEntities 
        { 
            get
            {
                imperialEntities = new List<Entity>();
                
                foreach (EntityControl imperial in ImperialControls)
                {
                    imperialEntities.Add(imperial.entity);
                }

                return imperialEntities;
            }   
        }
        public List<Entity> Rebels { get => rebels; set => rebels = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Intended to be called each game tick
        /// Each imperial entity has a random chance of acting within this method
        /// </summary>
        public void EnemyAct()
        {
            foreach (EntityControl imperial in imperialControls)
            {
                if (AIrand.Next(0, 200) == 0)
                {
                    EnemyAttack(imperial);
                }
                else if (AIrand.Next(0, 100) == 0)
                {
                    EnemyBlock(imperial);
                }
                else if (AIrand.Next(0, 200) == 0)
                {
                    EnemySpecial(imperial);
                }
                else if (AIrand.Next(0, 200) == 0)
                {
                    MoveEventArgs moveArgs = new MoveEventArgs(true, false);
                    MoveAI(imperial, moveArgs);
                }

            }
        }

        /// <summary>
        /// Entity passed in performs their primary attack
        /// </summary>
        /// <param name="imperial"></param>
        private void EnemyAttack(EntityControl imperial)
        {
            if (imperial.entity.ActionCooldown == 0)
            {
                if (imperial.entity.GetType() == typeof(Stormtrooper))
                {
                    imperial.entity.Target = rebels[AIrand.Next(0, rebels.Count)];
                    imperial.entityPicture.Image = Properties.Resources.Stormtrooper_Fire;
                    ((Stormtrooper)imperial.entity).blast();
                }
                else if (imperial.entity.GetType() == typeof(SecurityDroid))
                {
                    imperial.entity.SetMeleeTarget(rebels);
                    ((SecurityDroid)imperial.entity).Punch();
                }
                else if (imperial.entity.GetType() == typeof(Sith))
                {
                    imperial.entity.SetMeleeTarget(rebels);
                    imperial.entityPicture.Image = Properties.Resources.Inquisitor_Slash;
                    ((Sith)imperial.entity).LightSaberStrike();
                }
            }
        }

        /// <summary>
        /// Entity passed in performs their defend/block action
        /// </summary>
        /// <param name="imperial"></param>
        private void EnemyBlock(EntityControl imperial)
        {
            if (imperial.entity.ActionCooldown == 0)
            {
                if (imperial.entity.GetType() == typeof(SecurityDroid))
                {
                    MoveEventArgs moveArgs = new MoveEventArgs(false, true);
                    MoveAI(imperial, moveArgs);
                }
                if (imperial.entity.GetType() == typeof(Sith))
                {
                    imperial.entityPicture.Image = Properties.Resources.Inquisitor_Block;
                    ((Sith)imperial.entity).Blocking = true;
                    imperial.entity.ActionCooldown = AIrand.Next(imperial.entity.ActTime, 2 * imperial.entity.ActTime);
                }
            }
        }

        /// <summary>
        /// Enemy passed in performs their special
        /// </summary>
        /// <param name="imperial"></param>
        private void EnemySpecial(EntityControl imperial)
        {
            if (imperial.entity.ActionCooldown == 0 && imperial.entity.SpecialCooldown == 0)
            {
                if (imperial.entity.GetType() == typeof(SecurityDroid))
                {
                    imperial.entity.SetMeleeTarget(rebels);
                    ((SecurityDroid)imperial.entity).GroundSlam();
                }
                else if (imperial.entity.GetType() == typeof(Sith))
                {
                    imperial.entityPicture.Image = Properties.Resources.Inquisitor_Choke;
                    ((Sith)imperial.entity).ForceChoke(rebels);
                }
            }
        }
        #endregion
    }
}
