using System.Collections.Generic;

namespace KarpysDev.Script.Damage
{
    //Obsolete ?
    public interface IDamage
    {
        public void ApplyDamage(IDamageReceiver damageReceiver);
    }
}