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
        [SerializeField] private CircleDelayCollisionInitializer m_FriendlyCircleDelayCollisionInitializer = null;

        private void Awake()
        {
            m_FriendlyCircleCollisionInitializer.InitializeDictionary();
        }

        public void CreateCircleCollision(EntityGroup entityGroup, Vector3 position, Action<ITargetable> onCollisionDetected, CollisionType collisionType, float radius, Transform parent = null)
        {
            CircleCollisionInitializer circleCollisionInitializer = null;
            
            //Todo : Need to implement difference between friendly and other collision type
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

        public void CreateCircleContinuousDelayCollision(EntityGroup entityGroup, Vector3 position, Action<ITargetable> onCollisionDetected, float radius, float delayTick, Transform parent = null)
        {
            CircleDelayCollisionInitializer circleCollisionInitializer = null;
            
            if (entityGroup == EntityGroup.Friendly)
            {
                circleCollisionInitializer = Instantiate(m_FriendlyCircleDelayCollisionInitializer, position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }
            else
            {
                circleCollisionInitializer = Instantiate(m_FriendlyCircleDelayCollisionInitializer, position, Quaternion.identity, parent ? parent : m_CollisionParent);
            }

            circleCollisionInitializer.Initialize(radius, delayTick, onCollisionDetected);
        }
    }
}