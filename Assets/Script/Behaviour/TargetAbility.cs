using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;

namespace KarpysDev.Script.Behaviour
{
    public abstract class TargetAbility : Ability
    {
        protected PlayerPointTargetableSpellRule m_PlayerPointTargetableSpellRule = null;
        protected ITargetable m_Targetable = null;

        protected TargetAbility(ISource source, PlayerPointTargetableSpellRule spellRule) : base(source, spellRule)
        {
            m_PlayerPointTargetableSpellRule = spellRule;
        }

        protected override void Trigger()
        {
            m_Targetable = m_PlayerPointTargetableSpellRule.Targetable;
        }

        protected override bool IsRuleComplete()
        {
            return m_PlayerPointTargetableSpellRule.IsCompelte;
        }

        protected override bool IsSpellCanBeCast()
        {
            return true;
        }
    }
}