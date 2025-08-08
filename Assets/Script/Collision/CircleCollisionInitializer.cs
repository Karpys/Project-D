namespace KarpysDev.Script.Collision
{
    using System;
    using Collider;
    using Player;
    using UnityEngine;

    public class CircleCollisionInitializer : MonoBehaviour
    {
        [SerializeField] private CircleCollider m_CircleCollider = null;
        [SerializeField] private BaseCollision m_BaseCollision = null;
        [SerializeField] private Transform m_Visual = null;

        public void Initialize(float radius, Action<ITargetable> onCollisionDetected)
        {
            m_CircleCollider.Radius = radius;
            m_BaseCollision.AddCollisionAction(onCollisionDetected);
            m_BaseCollision.Activate();
            m_Visual.localScale = new Vector3(radius, 1, radius);
        }
    }
}