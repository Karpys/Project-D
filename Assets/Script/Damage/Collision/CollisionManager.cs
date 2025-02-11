namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using KarpysUtils;
    using Player;
    using UnityEngine;

    public class CollisionManager : SingletonMonoBehavior<CollisionManager>
    {
        [SerializeField] private Transform m_CollisionParent = null;

        [SerializeField] private CircleCollisionInitializer m_FriendlyCircleCollisionInitializer = null;
        [SerializeField] private CircleCollisionInitializer m_EnemyCircleCollisionInitializer = null;
        
        public void CreateCircleCollision(EntityGroup entityGroup, Vector3 position, Action<ITargetable> onCollisionDetected, float radius,Transform parent = null)
        {
            CircleCollisionInitializer circleCollisionInitializer = null;
            
            if (entityGroup == EntityGroup.Friendly)
            {
                circleCollisionInitializer = Instantiate(m_FriendlyCircleCollisionInitializer, position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }
            else
            {
                circleCollisionInitializer = Instantiate(m_EnemyCircleCollisionInitializer, position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }

            circleCollisionInitializer.Initialize(radius,onCollisionDetected);
        }
    }
}