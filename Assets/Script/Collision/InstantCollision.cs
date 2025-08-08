namespace KarpysDev.Script.Collision
{
    using Collider;

    public class InstantCollision : BaseCollision
    {
        private void FixedUpdate()
        {
            if(!m_Active)
                return;
            
            m_Collider.SetActive(true);
            m_Collider.Check(OnCollision);
            Disable();
            Destroy(gameObject,1f);
        }
    }
}