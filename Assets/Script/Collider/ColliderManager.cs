namespace  KarpysDev.Script.Collider
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class ColliderManager
    {
        public static Color GizmosColor = new Color(0.28f, 1f, 0.01f); 
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
        
        public static void Check(this BaseCollider baseCollider, Action<BaseCollider> onCollision)
        {
            CollisionCheck(baseCollider,onCollision);
        }

        public static bool CircleSquareCheck(CircleCollider circleCollider, SquareCollider squareCollider)
        {
            Vector3 circlePos = circleCollider.transform.position;
            Vector3 squarePos = squareCollider.transform.position;
            
            float halfWidth = squareCollider.Width / 2f;
            float halfHeight = squareCollider.Height / 2f;
            
            float closestX = Mathf.Clamp(circlePos.x, squarePos.x - halfWidth, squarePos.x + halfWidth);
            float closestZ = Mathf.Clamp(circlePos.z, squarePos.z - halfHeight, squarePos.z + halfHeight);

            float distanceX = circlePos.x - closestX;
            float distanceZ = circlePos.z - closestZ;
            float distanceSquared = distanceX * distanceX + distanceZ * distanceZ;

            return distanceSquared <= circleCollider.Radius * circleCollider.Radius;
        }
    }
}