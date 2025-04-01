namespace Script.Fx
{
    using UnityEngine;

    public class TormentPulseFx : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_LineRenderer = null;
        [SerializeField] private float m_PulseTime = 1;

        private Transform m_PointA = null;
        private Transform m_PointB = null;
        private float m_LineWidth = 1;

        private void Awake()
        {
            m_LineWidth = m_LineRenderer.startWidth;
        }

        public void Initialize(Transform pointA, Transform pointB)
        {
            m_PointA = pointA;
            m_PointB = pointB;
            m_LineRenderer.positionCount = 2;
            m_LineRenderer.material.SetFloat("_PulseSpeed",1/m_PulseTime);
            m_LineRenderer.material.SetFloat("_StartTime",Time.time);
            Invoke("Return",m_PulseTime);
        }

        private void Update()
        {
            m_LineRenderer.SetPosition(0,m_PointA.position);
            m_LineRenderer.SetPosition(1,m_PointB.position);
            
            m_LineRenderer.material.SetFloat("_Scale",ComputeScale());
        }

        private float ComputeScale()
        {
            float dist = Vector3.Distance(m_PointA.position, m_PointB.position);
            return dist / m_LineWidth;
        }

        private void Return()
        {
            Destroy(gameObject);
        }
    }
}