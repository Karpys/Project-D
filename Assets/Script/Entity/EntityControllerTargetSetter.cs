namespace KarpysDev.Script.Player
{
    using UnityEngine;

    public class EntityControllerTargetSetter : MonoBehaviour
    {
        [SerializeField] private Transform m_Target = null;
        [SerializeField] private EntityController m_EntityController = null;

        private void Start()
        {
            ITargetable targetable = m_Target.GetComponentInChildren<ITargetable>();

            if (targetable != null)
            {
                m_EntityController.SetTarget(targetable);
                m_EntityController.StartMovement();
            }
        }
    }
}