using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class PlayerEntity : BaseEntity
    {
        [Header("Player Entity Specifics")]
        [SerializeField] private LookAt m_LookAt = null;
        
        private AutoAttack m_AutoAttack = null;
        private Ability m_SpinAuto = null;

        public AutoAttack AutoAttack => m_AutoAttack;
        public Ability SpinAuto => m_SpinAuto;
        private void Awake()
        {
            m_AutoAttack = new AutoAttack(this,0.5f,0.2f);
            m_SpinAuto = new SpinAuto(this, m_LookAt);
        }

        private void Update()
        {
            m_AutoAttack.Update();
        }
    }
}