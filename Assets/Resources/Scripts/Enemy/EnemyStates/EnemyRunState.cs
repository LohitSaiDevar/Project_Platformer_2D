using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyRunState : IEnemyState
    {
        public EnemyStateID GetID()
        {
            return EnemyStateID.Run;
        }
        public void Enter(Enemy enemy)
        {

        }
        public void Update(Enemy enemy)
        {

        }
        public void Exit(Enemy enemy)
        {

        }

        public void FixedUpdate(Enemy enemy)
        {

        }
    }
}