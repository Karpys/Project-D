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

        #region SquareSquare
        public static bool SquareSquareCheck(SquareCollider squareCollider, SquareCollider squareCollider1)
        {
            Vector3[] cornersA = GetRotatedCorners(squareCollider);
            Vector3[] cornersB = GetRotatedCorners(squareCollider1);

            Vector3[] axes = {
                GetEdgeNormal(cornersA[0], cornersA[1]),
                GetEdgeNormal(cornersA[1], cornersA[2]),
                GetEdgeNormal(cornersB[0], cornersB[1]),
                GetEdgeNormal(cornersB[1], cornersB[2])
            };

            foreach (var axis in axes)
            {
                if (!ProjectionsOverlap(cornersA, cornersB, axis))
                    return false;
            }

            return true;
        }


        private static Vector3[] GetRotatedCorners(SquareCollider square)
        {
            float halfW = square.Width * 0.5f;
            float halfH = square.Height * 0.5f;
            float angleRad = -square.transform.eulerAngles.y * Mathf.Deg2Rad;
            
            Vector3 center = square.transform.position;

            return new Vector3[]
            {
                center + RotatePoint(new Vector3(-halfW, 0, -halfH), angleRad),
                center + RotatePoint(new Vector3(halfW, 0, -halfH), angleRad),
                center + RotatePoint(new Vector3(halfW, 0, halfH), angleRad),
                center + RotatePoint(new Vector3(-halfW, 0, halfH), angleRad)
            };
        }

        private static Vector3 RotatePoint(Vector3 point, float angleRad)
        {
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            return new Vector3(
                point.x * cos - point.z * sin,
                0,
                point.x * sin + point.z * cos
            );
        }

        private static Vector3 GetEdgeNormal(Vector3 p1, Vector3 p2)
        {
            Vector3 edge = p2 - p1;
            return new Vector3(-edge.z, 0, edge.x).normalized;
        }

        private static bool ProjectionsOverlap(Vector3[] cornersA, Vector3[] cornersB, Vector3 axis)
        {
            float minA, maxA, minB, maxB;
            ProjectCorners(cornersA, axis, out minA, out maxA);
            ProjectCorners(cornersB, axis, out minB, out maxB);

            return maxA >= minB && maxB >= minA;
        }

        private static void ProjectCorners(Vector3[] corners, Vector3 axis, out float min, out float max)
        {
            min = max = Vector3.Dot(corners[0], axis);
            for (int i = 1; i < corners.Length; i++)
            {
                float projection = Vector3.Dot(corners[i], axis);
                if (projection < min) min = projection;
                if (projection > max) max = projection;
            }
        }

        #endregion
    }
}