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
    
            float halfWidth = squareCollider.Width * 0.5f;
            float halfHeight = squareCollider.Height * 0.5f;
            float radiusSq = circleCollider.Radius * circleCollider.Radius;

            float angleRad = squareCollider.transform.eulerAngles.y * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);

            float localX = cos * (circlePos.x - squarePos.x) - sin * (circlePos.z - squarePos.z);
            float localZ = sin * (circlePos.x - squarePos.x) + cos * (circlePos.z - squarePos.z);

            float closestX = Mathf.Clamp(localX, -halfWidth, halfWidth);
            float closestZ = Mathf.Clamp(localZ, -halfHeight, halfHeight);

            float distanceX = localX - closestX;
            float distanceZ = localZ - closestZ;
            float distanceSquared = distanceX * distanceX + distanceZ * distanceZ;
            
            return distanceSquared <= radiusSq;
        }
    }
}