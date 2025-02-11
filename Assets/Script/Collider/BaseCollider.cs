namespace  KarpysDev.Script.Collider
{
    using UnityEngine;
    public abstract class BaseCollider : MonoBehaviour
    {
        [SerializeField] protected bool m_IsActive = true;
        [SerializeField] protected bool m_AutoRegister = true;
        [SerializeField] protected bool m_AutoDelete = true;
        public abstract bool IsColliding(BaseCollider collider);
        
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
        public abstract bool SquareCheck(SquareCollider squareCollider);

        public abstract bool CircleCheck(CircleCollider circleCollider);
        #endregion
    }
}
