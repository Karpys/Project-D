namespace KarpysDev.Script.Damage.Collision
{
    public class InstantCollision : BaseCollision
    {
        private bool m_IsActivate = false;
        private bool m_ReadyToDisable = false;

        private void FixedUpdate()
        {
            if(!m_IsActivate)
                return;
            
            if (m_ReadyToDisable)
            {
                m_IsActivate = false;
                Disable();
                Destroy(gameObject,1f);
                return;
            }
            
            m_ReadyToDisable = true;
        }
        
        public override void Activate()
        {
            base.Activate();
            m_IsActivate = true;
        }
    }
}