namespace KarpysDev.Script.Damage
{
    public class DamageSource
    {
        private DamageType m_DamageType = DamageType.Physical;
        private float m_Damage = 0f;

        public DamageType DamageType => m_DamageType;
        public float Damage => m_Damage;

        public DamageSource(float ammount, DamageType damageType)
        {
            m_DamageType = damageType;
            m_Damage = ammount;
        }

        public override string ToString()
        {
            return m_DamageType.ToString() + " " + m_Damage;
        }
    }
}