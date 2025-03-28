namespace KarpysDev.Script.Entity
{
    using UnityEngine;

    public class DefaultRoot : MonoBehaviour,IRoot
    {
        [SerializeField] private Transform m_Root = null;
        [SerializeField] private Transform m_Chest = null;
        [SerializeField] private Transform m_Head = null;
        public Transform GetRoot(RootPosition rootPosition)
        {
            switch (rootPosition)
            {
                case RootPosition.Root:
                    return m_Root;
                case RootPosition.Head:
                    return m_Head;
                case RootPosition.Chest:
                    return m_Chest;
            }

            return m_Root;
        }
    }
}