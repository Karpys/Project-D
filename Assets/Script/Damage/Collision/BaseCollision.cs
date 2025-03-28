namespace KarpysDev.Script.Damage.Collision
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
        private Action<ITargetable> A_OnCollisionDetected = null;

        public Action<ITargetable> OnCollisionDetected
        {
            get => A_OnCollisionDetected;
            set => A_OnCollisionDetected = value;
        }

        private void Awake()
        {
            if(m_Active)
                Activate();
        }

        protected void OnCollision(BaseCollider collider)
        {
            m_Collider.transform.name.Log("Collision Detected with : " + collider.transform.name);
            ITargetable damageTargetable = collider.GetComponentInChildren<ITargetable>();
            if(damageTargetable != null)
                A_OnCollisionDetected?.Invoke(damageTargetable);
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
    }
}