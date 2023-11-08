namespace KarpysDev.Script.Behaviour
{
    public class NoRule : SpellRule
    {
        public NoRule()
        {
            m_IsComplete = true;
        }
        public override void GetInfo()
        {
            return;
        }
    }
}