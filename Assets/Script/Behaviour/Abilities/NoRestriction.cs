namespace KarpysDev.Script.Behaviour
{
    public class NoRestriction : AbilityRestriction
    {
        public override bool IsAbilityReady()
        {
            return true;
        }

        public override void OnTrigger()
        {
            return;
        }
    }
}