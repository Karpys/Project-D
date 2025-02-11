namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using KarpysUtils;
    using Player;
    using UnityEngine;
    
    public class CollisionManager : SingletonMonoBehavior<CollisionManager>
    {
        [SerializeField] private Transform m_CollisionParent = null;
        [SerializeField] private GenericLibrary<CollisionType, CircleCollisionInitializer> m_FriendlyCircleCollisionInitializer = null;

        private void Awake()
        {
            m_FriendlyCircleCollisionInitializer.InitializeDictionary();
        }

        public void CreateCircleCollision(EntityGroup entityGroup, Vector3 position, Action<ITargetable> onCollisionDetected, CollisionType collisionType, float radius, Transform parent = null)
        {
            CircleCollisionInitializer circleCollisionInitializer = null;
            
            if (entityGroup == EntityGroup.Friendly)
            {
                circleCollisionInitializer = Instantiate(m_FriendlyCircleCollisionInitializer.GetViaKey(collisionType), position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }
            else
            {
                circleCollisionInitializer = Instantiate(m_FriendlyCircleCollisionInitializer.GetViaKey(collisionType), position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }

            circleCollisionInitializer.Initialize(radius,onCollisionDetected);
        }
    }
}