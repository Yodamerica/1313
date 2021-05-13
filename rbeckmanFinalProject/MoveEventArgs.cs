using System;
using System.Collections.Generic;
using System.Text;

namespace rbeckmanFinalProject
{
    /// <summary>
    /// Custom class for entity movement, extends EventArgs
    /// </summary>
    class MoveEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="retreat"></param>
        /// <param name="forward"></param>
        public MoveEventArgs(bool forward, bool retreat)
        {
            Forward = forward;
            Retreat = retreat;
        }

        public bool Forward { get; set; }
        public bool Retreat { get; set; }
    }
}
