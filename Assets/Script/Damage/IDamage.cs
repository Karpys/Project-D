using System.Collections.Generic;

namespace KarpysDev.Script.Damage
{
    public interface IDamage
    {
        public void ApplyDamage(IDamageReceiver damageReceiver);
    }
}