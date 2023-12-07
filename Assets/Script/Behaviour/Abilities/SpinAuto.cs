using KarpysDev.KarpysUtils.TweenCustom;
using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class SpinAuto : Ability,IDamage
    {
        private BaseEntity m_Controller = null;
        private LookAt m_LookAt = null;
        public SpinAuto(ISource source, AbilityRule abilityRule) : base(source,abilityRule)
        {
            if (source is EntitySource entitySource)
            {
                m_Controller = entitySource.Entity;
                m_LookAt = entitySource.EntityController.LookAt;
            }
        }

        protected override void Trigger()
        {
            if(!m_LookAt || !m_Controller)
                return;
            
            m_LookAt.ChangeLockCount(1);
            m_Controller.Animator.PlayTopAnimation("SpinSword",0.25f);
            m_Controller.Root.transform.DoRotate(new Vector3(0, 360,0),.3f).SetMode(TweenMode.ADDITIVE).OnComplete(() =>
            {
                m_LookAt.ChangeLockCount(-1);
                m_Controller.Animator.PlayTopAnimation("HoldSword",0.15f);
            });
        }

        protected override bool IsSpellCanBeCast()
        {
            return true;
        }

        public void ApplyDamage(IDamageReceiver damageReceiver)
        {
            damageReceiver.ReceiveDamage(new DamageSource(50f,DamageType.Physical),m_Source);
        }
    }
}