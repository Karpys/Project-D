namespace KarpysDev.Script.Behaviour
{
    public abstract class AbilityRule
    {
        protected Ability m_Ability = null;
        public abstract bool IsCompelte();
        public abstract void GetInfo();

        public void SetAbility(Ability ability)
        {
            m_Ability = ability;
        }
    }
}