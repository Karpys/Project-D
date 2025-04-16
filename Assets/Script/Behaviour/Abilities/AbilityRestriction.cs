namespace KarpysDev.Script.Behaviour
{
    public abstract class AbilityRestriction
    {
        public abstract bool IsAbilityReady();

        public abstract void OnTrigger();
    }
}