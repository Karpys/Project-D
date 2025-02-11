namespace KarpysDev.Script.Behaviour.Projectile
{
    using Damage;
    using Player;
    using UnityEngine;

    public class DamageCollisionEffect : OnCollisionEffect
    {
        [SerializeField] private DamageSource m_DamageSource = null;
        private bool m_HasCollide = false;
        public override void OnCollision(ITargetable targetable, ISource source)
        {
            if (m_HasCollide)
                return;

            if (targetable is IDamageTargetable damageTargetable)
            {
                damageTargetable.DamageReceiver.ReceiveDamage(m_DamageSource, source);
                m_HasCollide = true;
                //Todo: Correctly destroy projectile
                Destroy(gameObject);
            }
        }
    }
}