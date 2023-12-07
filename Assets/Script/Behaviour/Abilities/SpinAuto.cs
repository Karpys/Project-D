using KarpysDev.KarpysUtils.TweenCustom;
using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class SpinAuto : Ability,IDamage
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
        }

        protected override bool IsSpellCanBeCast()
        {
            return m_Source.Controller.CastLockCount <= 0;
        }

        public void ApplyDamage(IDamageReceiver damageReceiver)
        {
            damageReceiver.ReceiveDamage(new DamageSource(50f,DamageType.Physical),m_Source);
        }
    }
}