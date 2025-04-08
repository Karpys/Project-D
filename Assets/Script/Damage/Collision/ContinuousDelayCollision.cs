namespace KarpysDev.Script.Damage.Collision
{
    using Collider;
    using KarpysUtils;

    public class ContinuousDelayCollision : BaseCollision
    {
        private float m_TickDelay = 0f;
        private Clock m_CheckClock = null;

        public void Initialize(float tickDelay)
        {
            m_TickDelay = tickDelay;
            m_CheckClock = new Clock(m_TickDelay, Check);
        }

        private void Check()
        {
            m_Collider.Check(OnCollision);
            m_CheckClock.Restart(m_TickDelay);
        }
        private void FixedUpdate()
        {
            if(!m_Active)
                return;
            
            m_CheckClock.UpdateClock();
        }
    }
}