using System.Collections;
using KarpysDev.Script.Player;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class AutoAttack
    {
        private PlayerController m_Controller = null;
        private Clocker m_AutoAttackClock = null;
        private Clock m_LaunchAction = null;
        private float m_AttackLockNeeded = 0f;


        private bool m_IsCancelled = false;

        public Clocker AutoAttackClock => m_AutoAttackClock;

        public AutoAttack(PlayerController controller,float attackSpeed,float attackLockNeeded)
        {
            m_Controller = controller;
            m_AutoAttackClock = new Clocker(attackSpeed);
            m_AttackLockNeeded = attackLockNeeded;
        }
        
        public void LaunchAuto()
        {
            m_IsCancelled = false;
            m_AutoAttackClock.Launch();
            m_Controller.PlayerAnimation.PlayTopAnimation("Attack",0.25f);
            m_Controller.PlayerAnimation.PlayBotAnimation("Idle",0.25f);
            m_Controller.OnMovement += Cancelled;
            m_LaunchAction = new Clock(m_AttackLockNeeded, ApplyDamage);
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
                m_Controller.PlayerAnimation.PlayTopAnimation("HoldSword",0.1f);    
                return;
            }
            Debug.Log("Apply Damage");
        }
    }
}