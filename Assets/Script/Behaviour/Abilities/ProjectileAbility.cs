using KarpysDev.KarpysUtils;
using KarpysDev.KarpysUtils.TweenCustom;
using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class ProjectileAbility : Ability
    {
        private GameObject m_ProjectilePrefab = null;
        private GroundCastAbilityRule m_GroundCastAbilityRule = null;
        private float m_ProjectileSpeed = 0f;
        private Vector3 m_Offset = Vector3.zero;
        public ProjectileAbility(ISource source, GroundCastAbilityRule abilityRule,GameObject projectilePrefab,float projectileSpeed,Vector3 offset) : base(source, abilityRule)
        {
            m_ProjectilePrefab = projectilePrefab;
            m_GroundCastAbilityRule = abilityRule;
            m_ProjectileSpeed = projectileSpeed;
            m_Offset = offset;
        }

        protected override void Trigger()
        {
            if (m_Source is EntitySource entitySource)
            {
                entitySource.EntityController.LookAt.SetPoint(m_GroundCastAbilityRule.GroundHitPosition);
                GameObject proj = GameObject.Instantiate(m_ProjectilePrefab, entitySource.Root.transform.position + m_Offset, Quaternion.identity);
                entitySource.Animator.PlayTopAnimation("Attack",.25f,true);
                proj.transform.DoMove(m_GroundCastAbilityRule.GroundHitPosition + m_Offset,m_ProjectileSpeed.ToTime(Vector3.Distance(entitySource.Root.transform.position,m_GroundCastAbilityRule.GroundHitPosition)));
            }
        }

        protected override bool IsSpellCanBeCast()
        {
            return true;
        }
    }
}