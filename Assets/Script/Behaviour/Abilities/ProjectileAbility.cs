using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.MethodDelay;
using KarpysDev.KarpysUtils.TweenCustom;
using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class ProjectileAbility : Ability,IUpdater
    {
        private GameObject m_ProjectilePrefab = null;
        private GroundCastAbilityRule m_GroundCastAbilityRule = null;
        private Vector3 m_Offset = Vector3.zero;
        
        private float m_ProjectileSpeed = 0f;
        private float m_MovementLockTime = 0.5f;

        private IMethodDelayer m_MethodDelayer = new LinkedListMethodDelayer();
        public ProjectileAbility(ISource source, GroundCastAbilityRule abilityRule,GameObject projectilePrefab,float projectileSpeed,Vector3 offset) : base(source, abilityRule)
        {
            m_ProjectilePrefab = projectilePrefab;
            m_GroundCastAbilityRule = abilityRule;
            m_ProjectileSpeed = projectileSpeed;
            m_Offset = offset;
        }

        protected override void Trigger()
        {
            //Throw Projectile//
            if (m_Source is EntitySource entitySource)
            {
                entitySource.EntityController.LookAt.SetPoint(m_GroundCastAbilityRule.GroundHitPosition);
                GameObject proj = GameObject.Instantiate(m_ProjectilePrefab, entitySource.Root.transform.position + m_Offset, Quaternion.identity);
                entitySource.Animator.PlayTopAnimation("Attack",.25f,true);
                proj.transform.DoMove(m_GroundCastAbilityRule.GroundHitPosition + m_Offset,m_ProjectileSpeed.ToTime(Vector3.Distance(entitySource.Root.transform.position,m_GroundCastAbilityRule.GroundHitPosition)));
                
                entitySource.EntityController.ChangeMovementLockCount(1);
                m_MethodDelayer.AddDelayMethod(() => ReleaseLockCount(entitySource.EntityController),m_MovementLockTime);
            }
        }

        protected override bool IsSpellCanBeCast()
        {
            return true;
        }

        public void Update()
        {
            m_MethodDelayer.Update();
        }

        private void ReleaseLockCount(IController controller)
        {
            controller.ChangeMovementLockCount(-1);
        }
    }
}