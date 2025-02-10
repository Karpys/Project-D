namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using KarpysUtils;
    using Player;
    using UnityEngine;

    public class CollisionManager : SingletonMonoBehavior<CollisionManager>
    {
        [SerializeField] private Transform m_CollisionParent = null;

        [SerializeField] private BaseCollision m_FriendlyCircleCollision = null;
        [SerializeField] private BaseCollision m_EnemyCircleCollision = null;
        
        public void CreateCircleCollision(EntityGroup entityGroup, Vector3 position, Action<ITargetable> onCollisionDetected, float range)
        {
            BaseCollision baseCollision = null;
            
            if (entityGroup == EntityGroup.Friendly)
            {
                baseCollision = Instantiate(m_FriendlyCircleCollision, position, Quaternion.identity, m_CollisionParent);
            }
            else
            {
                baseCollision = Instantiate(m_EnemyCircleCollision, position, Quaternion.identity, m_CollisionParent);
            }

            baseCollision.transform.localScale = new Vector3(range, 1, range);
            baseCollision.OnCollisionDetected += onCollisionDetected;
            baseCollision.Activate();
        }
    }
}