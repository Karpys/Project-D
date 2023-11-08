using KarpysDev.Script.Damage;

namespace KarpysDev.Script.Behaviour
{
    public abstract class Ability
    {
        protected ISource m_Source = null;
        protected SpellRule m_SpellRule = null;

        protected Ability(ISource source,SpellRule spellRule)
        {
            m_Source = source;
            m_SpellRule = spellRule;
        }

        protected abstract void Trigger();
        protected abstract bool IsSpellCanBeCast();
        protected abstract bool IsRuleComplete();

        public void CastAbility()
        {
            if(!IsSpellCanBeCast())
                return;
            
            m_SpellRule.GetInfo();
            if(m_SpellRule.IsCompelte)
                Trigger();
        }
    }
}