namespace KarpysDev.Script.Behaviour.Projectile
{
    using Damage;
    using Player;
    using UnityEngine;

    public abstract class OnCollisionEffect : MonoBehaviour
    {
        public abstract void OnCollision(ITargetable targetable, ISource source);
    }
}