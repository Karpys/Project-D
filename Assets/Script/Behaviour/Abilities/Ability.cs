using KarpysDev.Script.Damage;

namespace KarpysDev.Script.Behaviour
{
    public abstract class Ability
    {
        protected ISource m_Source = null;
        protected AbilityRule m_AbilityRule = null;

        protected Ability(ISource source,AbilityRule abilityRule)
        {
            m_Source = source;
            m_AbilityRule = abilityRule;
            m_AbilityRule.SetAbility(this);
        }

        protected abstract void Trigger();
        protected abstract bool IsSpellCanBeCast();
        public void CastAbility()
        {
            if(!IsSpellCanBeCast())
                return;
            
            m_AbilityRule.GetInfo();
            if(m_AbilityRule.IsCompelte())
                Trigger();
        }

        public void DirectCastAbility()
        {
            if(!IsSpellCanBeCast())
                return;
            
            Trigger();
        }
    }
}