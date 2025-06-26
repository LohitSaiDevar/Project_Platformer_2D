using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyFallState : IEnemyState
    {
        public EnemyStateID GetID()
        {
            return EnemyStateID.Fall;
        }
        public void Enter(Enemy enemy)
        {
            enemy.ChangeAnimationState(Enemy.Enemy_Fall);
        }
        public void Update(Enemy enemy)
        {
            
        }
        public void FixedUpdate(Enemy enemy)
        {
            
        }
        public void Exit(Enemy enemy)
        {

        }
    }
}