namespace KarpysDev.Script.Collision
{
    using System;
    using Player;
    using UnityEngine;

    public abstract class BaseCollisionAction : MonoBehaviour
    {
        public abstract void AddCollisionAction(Action<ITargetable> action);

        public abstract void Invoke(ITargetable targetable);
    }
}