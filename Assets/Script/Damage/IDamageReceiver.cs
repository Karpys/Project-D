namespace KarpysDev.Script.Damage
{
    public interface IDamageReceiver
    {
        public void ReceiveDamage(DamageSource damageSource,ISource source);
    }
}