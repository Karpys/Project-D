namespace KarpysDev.Script.Behaviour
{
    using Damage;
    using Collision;
    using Entity;
    using Projectile;
    using UnityEngine;

    public class ProjectileTargetAbility : TargetAbility
    {
        private BaseProjectile m_BaseProjectilePrefab = null;
        
        public ProjectileTargetAbility(ISource source, PlayerPointTargetableAbilityRule abilityRule, AbilityRestriction abilityRestriction, BaseProjectile projectilePrefab,float range) : base(source, abilityRule,abilityRestriction)
        {
            m_BaseProjectilePrefab = projectilePrefab;
            abilityRule.SetRange(range);
        }

        protected override void Trigger()
        {
            base.Trigger();
            
            Vector3 spawnPosition = m_Source.Root.GetRoot(RootPosition.Root).transform.position;
            BaseProjectile proj = GameObject.Instantiate(m_BaseProjectilePrefab, spawnPosition, Quaternion.identity);
            Vector3 destination = m_Targetable.GetPivot.position;
            destination.y = spawnPosition.y;
            proj.Initialize(m_Source);
            proj.SetDestination(destination);
            CollisionManager.Instance.CreateCircleCollision(EntityGroup.Friendly, proj.transform.position, proj.OnCollision, CollisionType.Continuous, 1, proj.transform);
        }
    }
}