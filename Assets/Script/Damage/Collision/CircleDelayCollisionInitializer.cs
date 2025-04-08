namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using Collider;
    using Player;
    using UnityEngine;

    public class CircleDelayCollisionInitializer : MonoBehaviour
    {
        [SerializeField] private CircleCollider m_CircleCollider = null;
        [SerializeField] private ContinuousDelayCollision m_BaseCollision = null;
        [SerializeField] private Transform m_Visual = null;

        public void Initialize(float radius, float tickDelay, Action<ITargetable> onCollisionDetected)
        {
            m_CircleCollider.Radius = radius;
            m_BaseCollision.OnCollisionDetected += onCollisionDetected;
            m_BaseCollision.Activate();
            m_BaseCollision.Initialize(tickDelay);
            m_Visual.localScale = new Vector3(radius, 1, radius);
        }
    }
}