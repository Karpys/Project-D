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
            Gizmos.DrawWireCube(transform.position, new Vector3(m_Width, 0, m_Height));
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