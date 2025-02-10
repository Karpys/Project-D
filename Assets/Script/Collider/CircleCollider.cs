namespace  KarpysDev.Script.Collider
{
    using UnityEngine;

    public class CircleCollider : BaseCollider
    {
        [Header("Circle Specifics")]
        [SerializeField] private float m_Radius = 1;

        public float Radius
        {
            get => m_Radius;
            set => m_Radius = value;
        }
        public override bool IsColliding(BaseCollider collider)
        {
            if(!m_IsActive)
                return false;

            return collider.CircleCheck(this);
        }
        
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = ColliderManager.GizmosColor;
            DrawCircle(transform.position, m_Radius, 50);
        }

        private void DrawCircle(Vector3 center, float radius, int segments)
        {
            float angleStep = 2 * Mathf.PI / segments;
            Vector3 prevPoint = center + new Vector3(radius, 0, 0);

            for (int i = 1; i <= segments; i++)
            {
                float angle = i * angleStep;
                Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }
        #endif

        public override bool CircleCheck(CircleCollider circleCollider)
        {
            Vector3 pos = transform.position;
            Vector3 otherPos = circleCollider.transform.position;

            float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(otherPos.x, otherPos.z));
            float radiuses = m_Radius + circleCollider.Radius; 

            if (distance <= radiuses)
            {
                return true;
            }

            return false;
        }
        
        public override bool SquareCheck(SquareCollider squareCollider)
        {
            return ColliderManager.CircleSquareCheck(this, squareCollider);
        }
    }
}