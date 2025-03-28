namespace Script.Fx
{
    using UnityEngine;

    public class TormentPulseFx : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_LineRenderer = null;

        private Transform m_PointA = null;
        private Transform m_PointB = null;

        public void Initialize(Transform pointA, Transform pointB)
        {
            m_PointA = pointA;
            m_PointB = pointB;
            m_LineRenderer.positionCount = 2;
        }

        private void Update()
        {
            m_LineRenderer.SetPosition(0,m_PointA.position);
            m_LineRenderer.SetPosition(1,m_PointB.position);
        }
    }
}