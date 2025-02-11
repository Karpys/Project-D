namespace KarpysDev.Script.Collider
{
    using UnityEngine;

    public class SquareCollider : BaseCollider
    {
        [Header("Square Specifics")]
        [SerializeField] private float m_Width = 1;
        [SerializeField] private float m_Height = 1;

        public float Width => m_Width;
        public float Height => m_Height;
        
        public override bool IsColliding(BaseCollider collider)
        {
            if(!m_IsActive)
                return false;
            
            return collider.SquareCheck(this);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = ColliderManager.GizmosColor;
            
            float halfW = m_Width * 0.5f;
            float halfH = m_Height * 0.5f;
            float angleRad = -transform.eulerAngles.y * Mathf.Deg2Rad;

            Vector3[] corners = new Vector3[4];
            corners[0] = transform.position + RotatePoint(new Vector3(-halfW, 0, -halfH), angleRad);
            corners[1] = transform.position + RotatePoint(new Vector3(halfW, 0, -halfH), angleRad);
            corners[2] = transform.position + RotatePoint(new Vector3(halfW, 0, halfH), angleRad);
            corners[3] = transform.position + RotatePoint(new Vector3(-halfW, 0, halfH), angleRad);

            Gizmos.DrawLine(corners[0], corners[1]);
            Gizmos.DrawLine(corners[1], corners[2]);
            Gizmos.DrawLine(corners[2], corners[3]);
            Gizmos.DrawLine(corners[3], corners[0]);
        }
        
        private Vector3 RotatePoint(Vector3 point, float angleRad)
        {
            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);
            return new Vector3(point.x * cos - point.z * sin, 0, point.x * sin + point.z * cos);
        }
        
        #endif
        #region Circle
        public override bool CircleCheck(CircleCollider circleCollider)
        {
            return ColliderManager.CircleSquareCheck(circleCollider, this);
        }
        #endregion
        
        #region Square
        public override bool SquareCheck(SquareCollider squareCollider)
        {
            return false;
        }
        #endregion
    }
}