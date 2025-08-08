namespace KarpysDev.Script.Collision
{
    using System;
    using Collider;
    using KarpysUtils;
    using Player;
    using UnityEngine;

    public class BaseCollision : MonoBehaviour
    {
        [SerializeField] protected BaseCollider m_Collider = null;
        [SerializeField] protected bool m_Active = true;
        [SerializeField] private BaseCollisionAction m_CollisionAction = null;

        private void Awake()
        {
            if(m_Active)
                Activate();
        }

        protected void OnCollision(BaseCollider collider)
        {
            m_Collider.transform.name.Log("Collision Detected with : " + collider.transform.name);
            ITargetable targetable = collider.GetComponentInChildren<ITargetable>();
            if(targetable != null)
                m_CollisionAction?.Invoke(targetable);
        }

        public virtual void Activate()
        {
            m_Active = true;
            m_Collider.SetActive(true);
        }
        
        protected virtual void Disable()
        {
            m_Active = false;
            m_Collider.SetActive(false);
        }
        
        public void AddCollisionAction(Action<ITargetable> action)
        {
            m_CollisionAction.AddCollisionAction(action);
        }
    }
}