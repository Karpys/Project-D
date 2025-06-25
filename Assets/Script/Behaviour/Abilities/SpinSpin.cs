namespace KarpysDev.Script.Behaviour
{
    using Damage;
    using Damage.Collision;
    using Entity;
    using KarpysUtils;
    using KarpysUtils.TweenCustom;
    using Player;
    using UnityEngine;

    public class SpinSpin : Ability,IUpdater
    {
        private IAnimator m_Animator = null;
        private int m_SpinCount = 5;
        private int m_CurrentSpinCount = 0;
        private float m_SpinSpeed = 0.3f;
        private bool m_IsInSpin = false;

        private Loop m_SpinLoop = null;
        
        public SpinSpin(ISource source, AbilityRule abilityRule, AbilityRestriction abilityRestriction, float spinSpeed, int spinCount) : base(source, abilityRule, abilityRestriction)
        {
            if (source.Controller is IAnimator animator)
                m_Animator = animator;

            m_SpinSpeed = spinSpeed;
            m_SpinCount = spinCount;
        }

        protected override void Trigger()
        {
            if (m_IsInSpin)
            {
                StopSpin(true);
            }
            else
            {
                StartSpin();
            }
        }
        private void StartSpin()
        {
            m_IsInSpin = true;
            m_CurrentSpinCount = 0;
            Spin();
            m_SpinLoop = new Loop(m_SpinSpeed, Spin);
        }
        
        private void StopSpin(bool cancelAnim)
        {
            m_SpinLoop = null;
            m_IsInSpin = false;
            
            if(cancelAnim)
                m_Animator?.Animator.PlayTopAnimation("HoldSword",m_SpinSpeed);
        }

        private void Spin()
        {
            m_CurrentSpinCount += 1;
            m_Animator?.Animator.PlayTopAnimation("SpinSword",m_SpinSpeed);
            m_Source.Controller.LookAt.ChangeLockCount(1);
            m_Source.Root.GetRoot(RootPosition.Root).DoRotate(new Vector3(0, 360,0),m_SpinSpeed).SetMode(TweenMode.ADDITIVE).OnComplete(() =>
            {
                m_Source.Controller.LookAt.ChangeLockCount(-1);

                if (m_CurrentSpinCount == m_SpinCount)
                {
                    m_Animator?.Animator.PlayTopAnimation("HoldSword",m_SpinSpeed);
                }
            });
            

            if (m_CurrentSpinCount == m_SpinCount)
            {
                StopSpin(false);
            }
            
            CollisionManager.Instance.CreateCircleCollision(EntityGroup.Friendly,m_Source.Root.GetRoot(RootPosition.Root).position,ApplyDamage,CollisionType.Instant,3f);
        }

        private void ApplyDamage(ITargetable targetable)
        {
            if(targetable is IDamageTargetable damageTargetable)
                damageTargetable.DamageReceiver.ReceiveDamage(new DamageSource(20,DamageType.Physical),m_Source);
        }
        
        public void Update()
        {
            m_SpinLoop?.Update();
        }
    }
}