namespace KarpysDev.Script.Behaviour
{
    using Entity;
    using UnityEngine;

    public class SimpleRoot : IRoot
    {
        private Transform m_Transform = null;
        public SimpleRoot(Transform transform)
        {
            m_Transform = transform;
        }

        public Transform GetRoot(RootPosition rootPosition)
        {
            return m_Transform;
        }
    }
}