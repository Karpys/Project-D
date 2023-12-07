using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class PlayerEntity : BaseEntity
    {
        [Header("Player Entity Specifics")]
        [SerializeField] private GameObject m_ProjectilePrefab = null;
        [SerializeField] private float m_ProjectileSpeed = 0;
        
        private AutoAttack m_AutoAttack = null;
        private Ability m_SpinAuto = null;
        private ProjectileAbility m_Projectile = null;

        public AutoAttack AutoAttack => m_AutoAttack;
        public Ability SpinAuto => m_SpinAuto;
        public Ability Projectile => m_Projectile;
        protected override void Awake()
        {
            base.Awake();
            m_AutoAttack = new AutoAttack(m_Source,new PlayerPointTargetableAbilityRule(transform,m_AttackRange,m_Controller),0.5f,0.2f);
            m_SpinAuto = new SpinAuto(m_Source,new NoRule());
            m_Projectile = new ProjectileAbility(m_Source, new GroundCastAbilityRule(), m_ProjectilePrefab, m_ProjectileSpeed,new Vector3(0,1,0));
        }

        public void Update()
        {
            m_AutoAttack.Update();
            m_Projectile.Update();
        }
    }
}