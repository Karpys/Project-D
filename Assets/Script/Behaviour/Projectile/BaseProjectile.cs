using System;
using UnityEngine;

namespace KarpysDev.Script.Behaviour.Projectile
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float m_Speed = 0f;

        protected Vector3 m_Destination = Vector3.zero;
        protected bool m_Stop = false;

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
    }
}