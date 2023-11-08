namespace KarpysDev.Script.Behaviour
{
    public abstract class SpellRule
    {
        protected bool m_IsComplete = false;
        public bool IsCompelte => m_IsComplete;

        public abstract void GetInfo();
    }
}