using System.Collections;
using KarpysDev.Script.Player;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class AutoAttack:Ability,IUpdater
    {
        private BaseEntity m_Controller = null;
        private Clocker m_AutoAttackClock = null;
        private Clock m_LaunchAction = null;
        private float m_AttackLockNeeded = 0f;


        private bool m_IsCancelled = false;

        public AutoAttack(BaseEntity entity,float attackSpeed,float attackLockNeeded):base(entity)
        {
            m_Controller = entity;
            m_AutoAttackClock = new Clocker(attackSpeed);
            m_AttackLockNeeded = attackLockNeeded;
        }
        
        protected override void Trigger()
        {
            m_IsCancelled = false;
            m_AutoAttackClock.Launch();
            m_Controller.Animator.PlayTopAnimation("Attack",0.25f);
            m_Controller.Animator.PlayBotAnimation("Idle",0.25f);
            m_Controller.OnInterupt += Cancelled;
            m_LaunchAction = new Clock(m_AttackLockNeeded, ApplyDamage);
        }

        protected override bool CanTrigger()
        {
            return m_AutoAttackClock.IsReady;
        }

        private void Cancelled()
        {
            m_IsCancelled = true;
        }
        
        public void Update()
        {
            m_AutoAttackClock.UpdateClock();
            m_LaunchAction?.UpdateClock();
        }

        private void ApplyDamage()
        {
            if (m_IsCancelled)
            {
                //Todo:Play This anim only if player is in Attack State//
                m_Controller.Animator.PlayTopAnimation("HoldSword",0.1f);
                m_Controller.OnInterupt -= Cancelled;
                return;
            }
            Debug.Log("Apply Damage");
        }
    }
}