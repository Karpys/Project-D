namespace Script.Collision
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(Collider))]
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_OnEnter = null;
        [SerializeField] private UnityEvent m_OnExit = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_OnEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                m_OnExit?.Invoke();
            }
        }
    }
}