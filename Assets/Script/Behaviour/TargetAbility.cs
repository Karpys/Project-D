using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;

namespace KarpysDev.Script.Behaviour
{
    public abstract class TargetAbility : Ability
    {
        protected ITargetable m_Targetable = null;
        protected TargetAbility(ISource source) : base(source)
        {
            
        }

        public void SetTarget(ITargetable targetable)
        {
            m_Targetable = targetable;
        }
    }
}