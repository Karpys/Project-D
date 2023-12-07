using KarpysDev.KarpysUtils.MethodDelay;
using KarpysDev.Script.Behaviour.Projectile;
using KarpysDev.Script.Damage;
using KarpysDev.Script.Utils.ProjectUtils;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class ProjectileAbility : Ability,IUpdater
    {
        private BaseProjectile m_ProjectilePrefab = null;
        private GroundCastAbilityRule m_GroundCastAbilityRule = null;
        
        private float m_LockTime = 0.5f;
        private float m_ThrowDelay = 0.5f;

        private Clocker m_Cooldown = null;
        private IMethodDelayer m_MethodDelayer = new LinkedListMethodDelayer();
        public ProjectileAbility(ISource source, GroundCastAbilityRule abilityRule,BaseProjectile projectilePrefab,float lockTime,float launchDelay,float cooldown) : base(source, abilityRule)
        {
            m_ProjectilePrefab = projectilePrefab;
            m_GroundCastAbilityRule = abilityRule;
            m_LockTime = lockTime;
            m_ThrowDelay = launchDelay;
            m_Cooldown = new Clocker(cooldown);
        }

        protected override void Trigger()
        {
            //Throw Projectile//
            if (m_Source.Controller is IAnimator animator)
            {
                animator.Animator.PlayTopAnimation("Attack",.25f,true);
            }
            
            m_MethodDelayer.AddDelayMethod(() => ReleaseLockCount(m_Source.Controller),m_LockTime);
            m_MethodDelayer.AddDelayMethod(ThrowProjectile,m_ThrowDelay);
            m_Source.Controller.ChangeMovementLockCount(1);
            m_Source.Controller.ChangeCastLockCount(1);
            m_Cooldown.Launch();
        }

        private void ThrowProjectile()
        {
            m_Source.Controller.LookAt.SetPoint(m_GroundCastAbilityRule.GroundHitPosition);
            Vector3 spawnPosition = m_Source.SpawnRoot.transform.position;
            BaseProjectile proj = GameObject.Instantiate(m_ProjectilePrefab, spawnPosition, Quaternion.identity);
            proj.SetDestination(m_GroundCastAbilityRule.GroundHitPosition + new Vector3(0,spawnPosition.y,0));
        }

        protected override bool IsSpellCanBeCast()
        {
            return m_Cooldown.IsReady && m_Source.Controller.CastLockCount <= 0;
        }

        public void Update()
        {
            m_MethodDelayer.Update();
            m_Cooldown.UpdateClock();
        }

        private void ReleaseLockCount(IController controller)
        {
            controller.ChangeMovementLockCount(-1);
            controller.ChangeCastLockCount(-1);
        }
    }
}