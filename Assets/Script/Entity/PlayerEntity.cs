using KarpysDev.Script.Behaviour.Projectile;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    using System.Collections.Generic;
    using global::Script.Data.AbilityFielder;

    public class PlayerEntity : BaseEntity
    {
        [Header("Player Entity Specifics")] 
        [SerializeField] private AbilityScriptableObject m_ProjectilAbility = null;
        
        private AutoAttack m_AutoAttack = null;
        private Ability m_SpinAuto = null;
        private Ability m_Projectile = null;

        private List<IUpdater> m_AbilityUpdate = new List<IUpdater>();
        public AutoAttack AutoAttack => m_AutoAttack;
        public Ability SpinAuto => m_SpinAuto;
        public Ability Projectile => m_Projectile;
        protected override void Awake()
        {
            base.Awake();
            m_AutoAttack = new AutoAttack(m_Source,new PlayerPointTargetableAbilityRule(transform,m_AttackRange,m_Controller),0.5f,0.2f);
            m_SpinAuto = new SpinAuto(m_Source,new NoRule());
            //Add Spell Rule Giver, interface giver of GroundCast PlayerPoint ect ect//
            m_Projectile = m_ProjectilAbility.CreateBaseAbility(m_Source, new GroundCastAbilityRule());
            
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