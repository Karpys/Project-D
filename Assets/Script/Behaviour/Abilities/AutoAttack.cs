using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.MethodDelay;
using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;
using KarpysDev.Script.Utils.ProjectUtils;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class AutoAttack:TargetAbility,IUpdater
    {
        private BaseEntity m_Entity = null;
        private Clocker m_AutoAttackClock = null;
        private float m_AttackLockNeeded = 0f;
        
        private IMethodDelayer m_LaunchAction = new SingleMethodDelayer();
        private bool m_IsCancelled = false;

        public AutoAttack(ISource source,PlayerPointTargetableAbilityRule abilityRule,float attackSpeed,float attackLockNeeded):base(source,abilityRule)
        {
            if(source is EntitySource entitySource)
                m_Entity = entitySource.Entity;
            m_AutoAttackClock = new Clocker(attackSpeed);
            m_AttackLockNeeded = attackLockNeeded;
        }
        
        protected override void Trigger()
        {
            base.Trigger();
            
            m_IsCancelled = false;
            m_AutoAttackClock.Launch();
            m_LaunchAction.AddDelayMethod(ApplyDamage,m_AttackLockNeeded);
            
            if (m_Entity)
            {
                m_Entity.Animator.PlayTopAnimation("Attack",0.25f);
                m_Entity.Controller.SetLookAtTarget(m_Targetable.GetPivot);
                m_Entity.Controller.StopMovement();
                m_Entity.OnInterupt += Cancelled;
            }
        }

        protected override bool IsSpellCanBeCast()
        {
            return base.IsSpellCanBeCast() && m_AutoAttackClock.IsReady;
        }

        private void Cancelled()
        {
            m_IsCancelled = true;
        }
        
        public void Update()
        {
            m_AutoAttackClock.UpdateClock();
            m_LaunchAction.Update();
        }

        private void ApplyDamage()
        {
            if (m_IsCancelled)
            {
                //Todo:Play This anim only if player is in Attack State//
                m_Entity.Animator.PlayTopAnimation("HoldSword",0.1f);
                m_Entity.OnInterupt -= Cancelled;
                return;
            }

            if (m_PlayerPointTargetableAbilityRule.Targetable is IDamageTargetable damageTargetable)
            {
                damageTargetable.DamageReceiver.ReceiveDamage(new DamageSource(50f,DamageType.Physical),m_Source);
                Debug.Log("Apply Damage");
            }
        }
    }
}