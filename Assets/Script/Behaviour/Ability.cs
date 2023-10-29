namespace KarpysDev.Script.Behaviour
{
    public abstract class Ability
    {
        protected BaseEntity m_Entity = null;
        
        public Ability(BaseEntity entity)
        {
            m_Entity = entity;
        }
        
        protected abstract void Trigger();
        protected abstract bool CanTrigger();

        public void CastAbility()
        {
            if(CanTrigger())
                Trigger();
        }
    }
}