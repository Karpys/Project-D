using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;

namespace KarpysDev.Script.Behaviour
{
    public abstract class TargetAbility : Ability
    {
        protected PlayerPointTargetableAbilityRule m_PlayerPointTargetableAbilityRule = null;
        protected ITargetable m_Targetable = null;

        protected TargetAbility(ISource source, PlayerPointTargetableAbilityRule abilityRule, AbilityRestriction abilityRestriction) : base(source, abilityRule, abilityRestriction)
        {
            m_PlayerPointTargetableAbilityRule = abilityRule;
        }

        protected override void Trigger()
        {
            m_Targetable = m_PlayerPointTargetableAbilityRule.Targetable;
        }
    }
}