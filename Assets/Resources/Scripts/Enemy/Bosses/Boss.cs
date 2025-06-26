using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Bosses
{
    public class Boss : Enemy
    {
        public BossStateMachine bossStateMachine;
        [SerializeField] BossStateID initialBossState;
        [SerializeField] BossData bossData;
        public int currentHealth;
        public bool IsPhase1 {  get; set; }
        public bool IsPhase2 {  get; set; }
        public bool IsPhaseFinal {  get; set; }

        protected override void Awake()
        {
            base.Awake();
            RegisterAllBossStates();
            healthSystem = new HealthSystem_Enemy(bossData.MaxHP);
        }

        protected override void Update()
        {
            base.Update();
            bossStateMachine.Update();
            currentHealth = healthSystem.GetHealth();
            Debug.Log("health: " + currentHealth);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            bossStateMachine.FixedUpdate();
        }
        public void BossBarrier()
        {
            Debug.Log("Barrier Activated!");
        }

        protected void RegisterAllBossStates()
        {
            bossStateMachine = new BossStateMachine(this);
            bossStateMachine.RegisterState(new Phase1State());
            bossStateMachine.RegisterState(new Phase2State());
            bossStateMachine.RegisterState(new FinalPhaseState());
            bossStateMachine.ChangeState(initialBossState);
        }
    }
}