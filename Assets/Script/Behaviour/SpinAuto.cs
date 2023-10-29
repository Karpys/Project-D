using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class SpinAuto : Ability
    {
        private LookAt m_LookAt = null;
        public SpinAuto(BaseEntity entity,LookAt lookAt) : base(entity)
        {
            m_LookAt = lookAt;
        }

        protected override void Trigger()
        {
            m_LookAt.ChangeLockCount(1);
            m_Entity.Animator.PlayTopAnimation("SpinSword");
            m_Entity.Root.transform.DoRotate(new Vector3(0, 360,0),.3f).SetMode(TweenMode.ADDITIVE).OnComplete(() =>
            {
                m_LookAt.ChangeLockCount(-1);
                m_Entity.Animator.PlayTopAnimation("HoldSword");
            });
        }

        protected override bool CanTrigger()
        {
            return true;
        }
    }
}