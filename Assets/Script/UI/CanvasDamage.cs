namespace KarpysDev.Script.UI
{
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.ObjectPooling;
    using KarpysDev.Script.Damage;
    using UnityEngine;

    public class CanvasDamage : SingletonMonoBehavior<CanvasDamage>
    {
        [SerializeField] private UIDamageHolder m_UIDamageHolderPrefab = null;
        [SerializeField] private int m_InitialDamageHolder = 0;
        [SerializeField] private Transform m_Holder = null;

        private GameObjectPool<UIDamageHolder> m_DamageHolderPool = null;
        private void Awake()
        {
            m_DamageHolderPool = new GameObjectPool<UIDamageHolder>(m_UIDamageHolderPrefab, m_Holder,
                m_InitialDamageHolder, OnAddDamageHolder);
        }

        private void OnAddDamageHolder(UIDamageHolder obj)
        {
            return;
        }

        public void SpawnDamage(Transform origin, DamageSource damageSource)
        {
            return;
            UIDamageHolder damageHolder = m_DamageHolderPool.Take();
            damageHolder.Initialize(damageSource);
            damageHolder.Place(origin);
        }

        public void Return(UIDamageHolder uiDamageHolder)
        {
            m_DamageHolderPool.Return(uiDamageHolder);
        }
    }
}