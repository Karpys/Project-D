using UnityEngine;

namespace KarpysDev.Script.Behaviour.Projectile
{
    public class LinearProjectile : BaseProjectile
    {
        protected override void MoveTowardsDestination()
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Destination, m_Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Destination) < 0.1f)
                m_Stop = true;
        }
    }
}