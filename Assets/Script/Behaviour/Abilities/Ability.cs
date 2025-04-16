using KarpysDev.Script.Damage;

namespace KarpysDev.Script.Behaviour
{
    public abstract class Ability
    {
        protected ISource m_Source = null;
        protected AbilityRule m_AbilityRule = null;
        protected AbilityRestriction m_AbilityRestriction = null;

        protected Ability(ISource source, AbilityRule abilityRule,AbilityRestriction abilityRestriction)
        {
            m_Source = source;
            m_AbilityRule = abilityRule;
            m_AbilityRule.SetAbility(this);
            m_AbilityRestriction = abilityRestriction;
        }

        protected abstract void Trigger();

        protected bool IsSpellCanBeCast()
        {
            return m_AbilityRestriction.IsAbilityReady();
        }
        public void CastAbility()
        {
            if(!IsSpellCanBeCast())
                return;
            
            m_AbilityRule.GetInfo();
            if (m_AbilityRule.IsCompelte())
            {
                m_AbilityRestriction.OnTrigger();
                Trigger();
            }
        }

        public void DirectCastAbility()
        {
            if(!IsSpellCanBeCast())
                return;
            
            m_AbilityRestriction.OnTrigger();
            Trigger();
        }
    }
}