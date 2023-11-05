using KarpysDev.Script.Damage;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public interface IDamageTargetable : ITargetable
    {
        public IDamageReceiver DamageReceiver { get; }
    }
}