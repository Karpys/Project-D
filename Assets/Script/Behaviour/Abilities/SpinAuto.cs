using KarpysDev.KarpysUtils.TweenCustom;
using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    using Damage.Collision;
    using Player;

    public class SpinAuto : Ability
    {
        private IAnimator m_Animator = null;
        public SpinAuto(ISource source, AbilityRule abilityRule) : base(source,abilityRule)
        {
            if (source.Controller is IAnimator animator)
                m_Animator = animator;
        }

        protected override void Trigger()
        {
            m_Animator?.Animator.PlayTopAnimation("SpinSword",0.25f);
            m_Source.Controller.LookAt.ChangeLockCount(1);
            m_Source.Root.transform.DoRotate(new Vector3(0, 360,0),.3f).SetMode(TweenMode.ADDITIVE).OnComplete(() =>
            {
                m_Source.Controller.LookAt.ChangeLockCount(-1);
                m_Animator?.Animator.PlayTopAnimation("HoldSword",0.15f);
            });
            
            //Todo:Get the correct entity group
            CollisionManager.Instance.CreateCircleCollision(EntityGroup.Friendly,m_Source.Root.position,ApplyDamage,CollisionType.Instant,3f);
        }

        protected override bool IsSpellCanBeCast()
        {
            return m_Source.Controller.CastLockCount <= 0;
        }

        public void ApplyDamage(ITargetable targetable)
        {
            if(targetable is IDamageTargetable damageReceiver)
                damageReceiver.DamageReceiver.ReceiveDamage(new DamageSource(50f,DamageType.Physical),m_Source);
        }
    }
}