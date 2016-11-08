using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmup.Enemies
{
    class EnemyAction
    {
        // performs actions and returns the direction the enemy should be heading
        Func<Vector2, Tuple<Vector2, bool>> execute;

        // the delay before the enemy performs next action
        private int delay;

        public int Delay
        {
            get
            {
                return delay;
            }
        }

        public Tuple<Vector2, bool> Execute(Vector2 position)
        {
            return execute(position);
        }

        public EnemyAction(int delay, Func<Vector2, Tuple<Vector2, bool>> execute)
        {
            this.delay = delay;
            this.execute = execute;
        }
    }
}
