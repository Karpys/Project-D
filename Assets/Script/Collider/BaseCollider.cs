namespace  KarpysDev.Script.Collider
{
    using System;
    using UnityEngine;

    public enum ColliderType
    {
        Square,
        Circle,
    }
    public abstract class BaseCollider : MonoBehaviour
    {
        [SerializeField] protected bool m_IsActive = true;
        [SerializeField] protected bool m_AutoRegister = true;
        [SerializeField] protected bool m_AutoDelete = true;
        public abstract bool IsColliding(BaseCollider collider);
        
        public abstract ColliderType ColliderType { get; }

        public void SetActive(bool active)
        {
            m_IsActive = active;
        }

        private void Awake()
        {
            if(m_AutoRegister)
                ColliderManager.AddCollider(this);
        }

        private void OnDestroy()
        {
            if(m_AutoDelete)
                ColliderManager.RemoveCollider(this);
        }

        #region Circle
        public abstract bool SquareCheck(CircleCollider circleCollider);

        public abstract bool CircleCheck(CircleCollider circleCollider);
        #endregion
    }
}