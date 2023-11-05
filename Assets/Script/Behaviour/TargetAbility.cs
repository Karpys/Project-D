using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;

namespace KarpysDev.Script.Behaviour
{
    public abstract class TargetAbility : Ability
    {
        protected ITargetable m_Targetable = null;
        protected ITargetProvider m_TargetProvider = null;
        protected TargetAbility(ISource source,ITargetProvider targetProvider) : base(source)
        {
            m_TargetProvider = targetProvider;
        }

        protected override void Trigger()
        {
            m_Targetable = m_TargetProvider.Targetable;
        }

        protected override bool CanTrigger()
        {
            return m_TargetProvider.Targetable != null;
        }
    }
}