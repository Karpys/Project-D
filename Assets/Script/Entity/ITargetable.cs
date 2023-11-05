using UnityEngine;

namespace KarpysDev.Script.Player
{
    public interface ITargetable
    {
        Transform GetPivot { get; }
    }
}