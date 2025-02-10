namespace  KarpysDev.Script.Collider
{
    using KarpysUtils;
    using UnityEngine;

    public class CircleCollider : BaseCollider
    {
        [SerializeField] private float m_Radius = 0;

        public float Radius
        {
            get => m_Radius;
            set => m_Radius = value;
        }

        public override ColliderType ColliderType => ColliderType.Circle;

        public override bool IsColliding(BaseCollider collider)
        {
            if(!m_IsActive)
                return false;
            
            switch (collider.ColliderType)
            {
                case ColliderType.Square:
                    return collider.SquareCheck(this);
                case ColliderType.Circle:
                    return collider.CircleCheck(this);
                default:
                    return false;
            }
        }

        #region Circle
        public override bool SquareCheck(CircleCollider circleCollider)
        {
            return false;
        }

        public override bool CircleCheck(CircleCollider circleCollider)
        {
            Vector3 pos = transform.position;
            Vector3 otherPos = circleCollider.transform.position;

            float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(otherPos.x, otherPos.z));
            float radiuses = m_Radius + circleCollider.Radius; 

            if (distance < radiuses)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}