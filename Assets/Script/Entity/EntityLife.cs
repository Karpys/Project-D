namespace KarpysDev.Script.Behaviour
{
    using UnityEngine;

    public class EntityLife : MonoBehaviour
    {
        [SerializeField] private BaseEntity m_BaseEntity = null;
        [SerializeField] private float m_Life = 0;

        private float m_CurrentLife = 0;

        private void Start()
        {
            m_CurrentLife = m_Life;
        }

        public void TakeDamage(float amount)
        {
            m_CurrentLife -= amount;

            if (m_CurrentLife <= 0)
                TriggerDeath();
        }

        private void TriggerDeath()
        {
            m_BaseEntity.TriggerDeath();
        }
    }
}