using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class HealthSystem_Enemy
    {
        int health;
        int healthMax;
        public HealthSystem_Enemy(int health)
        {
            this.health = health;
            healthMax = health;
        }

        public int GetHealth()
        {
            return health;
        }

        public float GetHealthPercent()
        {
            return ((float)health / healthMax) * 100;
        }

        public void DamageTaken(int damage)
        {
            health -= damage;
            Debug.Log("health: " + health);
            Debug.Log("HPpercent: " + GetHealthPercent());

            if (health < 0)
            {
                health = 0;
            }
        }

        public void Heal(int healAmount)
        {
            health += healAmount;

            if (health > healthMax)
            {
                health = healthMax;
            }
        }
    }
}