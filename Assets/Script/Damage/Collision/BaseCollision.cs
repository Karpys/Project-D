namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using KarpysUtils;
    using Player;
    using UnityEngine;

    public class BaseCollision : MonoBehaviour
    {
        [SerializeField] private Collider m_Collider = null;
        private Action<ITargetable> A_OnCollisionDetected = null;

        public Action<ITargetable> OnCollisionDetected
        {
            get => A_OnCollisionDetected;
            set => A_OnCollisionDetected = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageTargetable damageTargetable = other.GetComponentInChildren<IDamageTargetable>();

            A_OnCollisionDetected?.Invoke(damageTargetable);
            damageTargetable?.GetPivot.name.Log("Collision Detected");
            
            //Todo:Add different type of Collision
            //Continue / Instant / Multiple / Single
        }

        public virtual void Activate()
        {
            m_Collider.enabled = true;
        }
        
        protected virtual void Disable()
        {
            m_Collider.enabled = false;
        }
    }
}