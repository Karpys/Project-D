namespace KarpysDev.Script.Damage.Collision
{
    using Collider;

    public class ContinuousCollision : BaseCollision
    {
        private void FixedUpdate()
        {
            if(!m_Active)
                return;
            m_Collider.Check(OnCollision);
        }
    }
}