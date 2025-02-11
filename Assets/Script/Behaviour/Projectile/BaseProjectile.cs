using System;
using UnityEngine;

namespace KarpysDev.Script.Behaviour.Projectile
{
    using Damage;
    using Player;

    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected OnCollisionEffect m_OnCollisionEffect = null;
        [SerializeField] protected float m_Speed = 0f;

        private ISource m_Source = null;
        protected Vector3 m_Destination = Vector3.zero;
        protected bool m_Stop = false;

        public void Initialize(ISource source)
        {
            m_Source = source;
        }
        public void SetDestination(Vector3 destination)
        {
            m_Destination = destination;
        }

        protected void Update()
        {
            if(m_Stop)
                return;
            MoveTowardsDestination();
        }

        protected abstract void MoveTowardsDestination();

        public void OnCollision(ITargetable targetable)
        {
            m_OnCollisionEffect.OnCollision(targetable,m_Source);
        }
    }
}