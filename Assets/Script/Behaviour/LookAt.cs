using System;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform m_Tranform = null;
        [SerializeField] private bool m_Active = true;
        
        private Vector3 m_Point;
        private Transform m_Target;
        private void Update()
        {
            if(!m_Active)
                return;
            
            if (m_Target)
                m_Point = m_Target.position;

            LookAtPoint();
        }

        private void LookAtPoint()
        {
            
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
            m_Active = true;
        }

        public void SetPoint(Vector3 point)
        {
            m_Point = point;
            m_Active = true;
        }

        public void Stop()
        {
            m_Active = false;
        }
    }
}