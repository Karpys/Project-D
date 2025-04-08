using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    using System.Collections.Generic;
    using global::Script.Data.AbilityFielder;

    public class PlayerEntity : BaseEntity
    {
        [Header("Player Entity Specifics")] 
        [SerializeField] private AbilityScriptableObject m_ProjectileAbilityScriptableObject = null;
        [SerializeField] private AbilityScriptableObject m_ProjectileTargetAbilityScriptableObject = null;
        [SerializeField] private AbilityScriptableObject m_TormentPulseAbilityScriptableObject = null;
        [SerializeField] private AbilityScriptableObject m_SpinSpinAbilityScriptableObject = null;
        
        private AutoAttack m_AutoAttack = null;
        private Ability m_SpinAuto = null;
        private Ability m_Projectile = null;
        private Ability m_ProjectileTarget = null;
        private Ability m_TormentPulse = null;
        private Ability m_SpinSpin = null;

        private List<IUpdater> m_AbilityUpdate = new List<IUpdater>();
        public AutoAttack AutoAttack => m_AutoAttack;
        public Ability SpinAuto => m_SpinAuto;
        public Ability Projectile => m_Projectile;
        public Ability ProjectileTarget => m_ProjectileTarget;
        public Ability TormentPulse => m_TormentPulse;
        public Ability SpinSpin => m_SpinSpin;

        protected override void Awake()
        {
            base.Awake();
            m_AutoAttack = new AutoAttack(m_Source,new PlayerPointTargetableAbilityRule(transform,m_AttackRange,m_Controller),0.5f,0.2f);
            m_SpinAuto = new SpinAuto(m_Source,new NoRule());
            //Add Spell Rule Giver, interface giver of GroundCast PlayerPoint ect//
            m_Projectile = m_ProjectileAbilityScriptableObject.CreateBaseAbility(m_Source, new GroundCastAbilityRule());
            m_ProjectileTarget = m_ProjectileTargetAbilityScriptableObject.CreateBaseAbility(m_Source, new PlayerPointTargetableAbilityRule(transform, m_AttackRange, m_Controller));
            m_TormentPulse = m_TormentPulseAbilityScriptableObject.CreateBaseAbility(m_Source, new NoRule());
            m_SpinSpin = m_SpinSpinAbilityScriptableObject.CreateBaseAbility(m_Source, new NoRule());
            
            AddAbility(m_AutoAttack);
            AddAbility(m_SpinAuto);
            AddAbility(m_Projectile);
            AddAbility(m_ProjectileTarget);
            AddAbility(m_TormentPulse);
            AddAbility(m_SpinSpin);
        }

        private void AddAbility(Ability ability)
        {
            if(ability is IUpdater updater)
                m_AbilityUpdate.Add(updater);
        }

        public void Update()
        {
            foreach (IUpdater updater in m_AbilityUpdate)
            {
                updater.Update();
            }
        }
    }
}