namespace KarpysDev.Script.Behaviour
{
    using global::Script;
    using KarpysUtils;

    public class CooldownRestriction : AbilityRestriction,IUpdater
    {
        private float m_CooldownTime = 0;
        private Clock m_CooldownClock = null;
        private bool m_IsReady = true;
        
        public float CooldownTime => m_CooldownTime;
        
        public CooldownRestriction(float time)
        {
            m_CooldownTime = time;
            m_CooldownClock = new Clock(m_CooldownTime, SetReady);
            UpdateManager.Instance.OnUpdate += Update;
        }

        ~CooldownRestriction()
        {
            if(UpdateManager.Instance)
                UpdateManager.Instance.OnUpdate -= Update;
        }

        private void SetReady()
        {
            m_IsReady = true;
        }

        public override bool IsAbilityReady()
        {
            return m_IsReady;
        }

        public override void OnTrigger()
        {
            m_IsReady = false;
            m_CooldownClock.SetTime(m_CooldownTime);
        }

        public void Update()
        {
            m_CooldownClock.UpdateClock();   
        }
    }
}