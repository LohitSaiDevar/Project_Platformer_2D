using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class AnimationEventTrigger_Enemy : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        public void AttackPlayer()
        {
            enemy.Attack();
        }
    }
}