namespace KarpysDev.Script.Damage.Collision
{
    using System;
    using KarpysUtils;
    using Player;
    using UnityEngine;

    public class BaseCollision : MonoBehaviour
    {
        [SerializeField] private Collider m_Collider = null;
        [SerializeField] private float m_DestroyDelay = 1f;
        private Action<ITargetable> A_OnCollisionDetected = null;

        public Action<ITargetable> OnCollisionDetected
        {
            get => A_OnCollisionDetected;
            set => A_OnCollisionDetected = value;
        }

        //Todo: Destroy correctly
        private void Start()
        {
            Destroy(gameObject,m_DestroyDelay);
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageTargetable damageTargetable = other.GetComponentInChildren<IDamageTargetable>();

            A_OnCollisionDetected?.Invoke(damageTargetable);
            damageTargetable?.GetPivot.name.Log("Collision Detected");
            
            //Todo:Add different type of Collision
            //Continue / Instant / Multiple / Single
        }

        public void Activate()
        {
            m_Collider.enabled = true;
        }
    }
}