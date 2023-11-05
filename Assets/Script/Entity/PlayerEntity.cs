using KarpysDev.Script.Damage;
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
        protected override void Awake()
        {
            base.Awake();
            m_AutoAttack = new AutoAttack(m_Source,0.5f,0.2f);
            m_SpinAuto = new SpinAuto(m_Source,m_LookAt);
        }

        public void Update()
        {
            m_AutoAttack.Update();
        }
    }
}