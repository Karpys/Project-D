using KarpysDev.Script.Damage;

namespace KarpysDev.Script.Behaviour
{
    public abstract class Ability
    {
        protected ISource m_Source = null;

        protected Ability(ISource source)
        {
            m_Source = source;
        }

        protected abstract void Trigger();
        protected abstract bool CanTrigger();

        public void CastAbility()
        {
            if (CanTrigger())
                Trigger();
        }
    }
}