namespace  KarpysDev.Script.Collider
{
    using System;
    using System.Collections.Generic;

    public static class ColliderManager
    {
        private static List<BaseCollider> m_Colliders = new List<BaseCollider>();

        public static void AddCollider(BaseCollider collider)
        {
            m_Colliders.Add(collider);
        }

        public static void RemoveCollider(BaseCollider collider)
        {
            m_Colliders.Remove(collider);
        }

        public static void CollisionCheck(BaseCollider collider, Action<BaseCollider> onCollision)
        {
            foreach (BaseCollider otherCollider in m_Colliders)
            {
                if(otherCollider == collider)
                    continue;
                
                if(collider.IsColliding(otherCollider))
                    onCollision?.Invoke(otherCollider);
            }
        }
    }
}