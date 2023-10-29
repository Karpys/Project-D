using System;
using KarpysDev.Script.Helper;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform m_Body = null;
        [SerializeField] private Transform m_Tranform = null;
        [SerializeField] private bool m_Active = true;

        [Header("Parameters")] 
        [SerializeField] private float m_Offset = 0f;
        [SerializeField] private float m_RotationSpeed = 0f;

        private int m_LockCount = 0;
        private Vector3 m_Point;
        private Transform m_Target;
        private void Update()
        {
            if(!m_Active || m_LockCount > 0)
                return;
            
            if (m_Target)
                m_Point = m_Target.position;

            LookAtPoint();
        }

        private void LookAtPoint()
        {
            Vector3 eulerAngles = m_Tranform.localEulerAngles;
            
            Vector3 direction = m_Point - m_Body.position;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float rotationSpeed = m_RotationSpeed * Time.deltaTime;
            float currentAngle = Mathf.MoveTowardsAngle(eulerAngles.y - m_Offset, targetAngle, rotationSpeed);

            eulerAngles = new Vector3(eulerAngles.x, currentAngle + m_Offset, eulerAngles.z);
            m_Tranform.localEulerAngles = eulerAngles;
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }

        public void SetPoint(Vector3 point)
        {
            m_Point = point;
        }

        public void Active(bool active)
        {
            m_Active = active;
        }

        public void ChangeLockCount(int lockCount)
        {
            m_LockCount += lockCount;
        }
    }
}