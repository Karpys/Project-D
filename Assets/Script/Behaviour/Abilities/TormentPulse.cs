namespace KarpysDev.Script.Behaviour
{
    using Damage;
    using Damage.Collision;
    using Entity;
    using global::Script.Fx;
    using KarpysUtils;
    using Player;

    public class TormentPulse : Ability,IUpdater
    {
        private bool m_InPulse = false;
        private Clock m_PulseClock = null;

        private float m_PulseDelay = 0.25f;
        private float m_PulseRadius = 1;
        private int m_MaxEnemy = 1;

        private int m_CurrentEnemyPulseCount = 0;
        public TormentPulse(ISource source, AbilityRule abilityRule, AbilityRestriction abilityRestriction, float pulseDelay, float pulseRadius, int maxEnemy) : base(source, abilityRule, abilityRestriction)
        {
            m_PulseDelay = pulseDelay;
            m_PulseRadius = pulseRadius;
            m_MaxEnemy = maxEnemy;
            m_PulseClock = new Clock(m_PulseDelay, TriggerPulse);
        }

        public void Update()
        {
            if(m_InPulse)
                m_PulseClock.UpdateClock();
        }
        
        protected override void Trigger()
        {
            TogglePulse();
        }

        private void TogglePulse()
        {
            m_InPulse = !m_InPulse;
            m_PulseClock.Restart(m_PulseDelay);
        }

        private void TriggerPulse()
        {
            m_PulseClock.Restart(m_PulseDelay);
            m_CurrentEnemyPulseCount = 0;
            //Directly Check Surrounding enemies instead of create collision
            CollisionManager.Instance.CreateCircleCollision(EntityGroup.Enemy,m_Source.Root.GetRoot(RootPosition.Root).position,OnCollision
                ,CollisionType.Instant,m_PulseRadius);
        }

        private void OnCollision(ITargetable targetable)
        {
            if (m_CurrentEnemyPulseCount == m_MaxEnemy)
                return;

            m_CurrentEnemyPulseCount += 1;
            
            if(targetable is IDamageTargetable damageTargetable)
                damageTargetable.DamageReceiver.ReceiveDamage(new DamageSource(20,DamageType.Ice),m_Source);
            
            FxManager.Instance.CreateTormentPulseFx(targetable.Source.Root.GetRoot(RootPosition.Chest),m_Source.Root.GetRoot(RootPosition.Chest));
        }
    }
}