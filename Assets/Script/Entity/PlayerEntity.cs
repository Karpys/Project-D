using KarpysDev.Script.Behaviour.Projectile;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    using System.Collections.Generic;
    using global::Script.Data.AbilityFielder;
    using UnityEngine.Serialization;

    public class PlayerEntity : BaseEntity
    {
        [Header("Player Entity Specifics")] 
        [SerializeField] private AbilityScriptableObject m_ProjectileAbility = null;
        [SerializeField] private AbilityScriptableObject m_ProjectileTargetAbility = null;
        
        private AutoAttack m_AutoAttack = null;
        private Ability m_SpinAuto = null;
        private Ability m_Projectile = null;
        private Ability m_ProjectileTarget = null;

        private List<IUpdater> m_AbilityUpdate = new List<IUpdater>();
        public AutoAttack AutoAttack => m_AutoAttack;
        public Ability SpinAuto => m_SpinAuto;
        public Ability Projectile => m_Projectile;
        public Ability ProjectileTarget => m_ProjectileTarget;

        protected override void Awake()
        {
            base.Awake();
            m_AutoAttack = new AutoAttack(m_Source,new PlayerPointTargetableAbilityRule(transform,m_AttackRange,m_Controller),0.5f,0.2f);
            m_SpinAuto = new SpinAuto(m_Source,new NoRule());
            //Add Spell Rule Giver, interface giver of GroundCast PlayerPoint ect ect//
            m_Projectile = m_ProjectileAbility.CreateBaseAbility(m_Source, new GroundCastAbilityRule());
            m_ProjectileTarget = m_ProjectileTargetAbility.CreateBaseAbility(m_Source, new PlayerPointTargetableAbilityRule(transform, m_AttackRange, m_Controller));
            
            if(m_Projectile is IUpdater updater)
                m_AbilityUpdate.Add(updater);
        }

        public void Update()
        {
            foreach (IUpdater updater in m_AbilityUpdate)
            {
                updater.Update();
            }
            m_AutoAttack.Update();
        }
    }
}