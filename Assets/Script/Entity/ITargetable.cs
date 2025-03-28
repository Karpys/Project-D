using UnityEngine;

namespace KarpysDev.Script.Player
{
    using Damage;

    public interface ITargetable
    {
        Transform GetPivot { get; }
        ISource Source { get; }
    }
}