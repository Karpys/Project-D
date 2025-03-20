namespace KarpysDev.Script.UI
{
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.ObjectPooling;
    using KarpysDev.Script.Damage;
    using KarpysUtils.TweenCustom;
    using UnityEngine;

    public class CanvasDamage : SingletonMonoBehavior<CanvasDamage>
    {
        [SerializeField] private Camera m_Camera = null;
        [SerializeField] private UIDamageHolder m_UIDamageHolderPrefab = null;
        [SerializeField] private int m_InitialDamageHolder = 0;
        [SerializeField] private Transform m_Holder = null;
        
        [Header("Place Parameters")]
        [SerializeField] private float m_ReturnDuration = 0.25f;
        [SerializeField] private Vector2 m_MinMaxLateral = Vector2.zero;
        [SerializeField] private Vector2 m_MinMaxHorizontal = Vector2.zero;
        [SerializeField] private float m_ScaleApparitionDuration = 0.25f;
        [SerializeField] private float m_ScaleAppearDuration = 0.25f;
        [SerializeField] private Ease m_ScaleAppearEase = Ease.LINEAR;
        [SerializeField] private float m_ScaleDisappearDuration = 0.25f;
        [SerializeField] private Ease m_ScaleDisappearEase = Ease.LINEAR;

        public static float RETURN_TEXT_DURATION = 0.75f;

        private GameObjectPool<UIDamageHolder> m_DamageHolderPool = null;
        private void Awake()
        {
            m_DamageHolderPool = new GameObjectPool<UIDamageHolder>(m_UIDamageHolderPrefab, m_Holder,
                m_InitialDamageHolder, OnAddDamageHolder);
        }

        private void OnAddDamageHolder(UIDamageHolder obj)
        {
            obj.transform.eulerAngles = m_Camera.transform.eulerAngles;
        }

        public void SpawnDamage(Transform origin, DamageSource damageSource)
        {
            UIDamageHolder damageHolder = m_DamageHolderPool.Take();
            damageHolder.Initialize(damageSource);
            Vector3 position = origin.position;
            position.x += Random.Range(m_MinMaxLateral.x, m_MinMaxLateral.y);
            position.z += Random.Range(m_MinMaxHorizontal.x, m_MinMaxHorizontal.y);
            damageHolder.transform.localScale = Vector3.zero;
            damageHolder.transform.DoScale(Vector3.one, m_ScaleAppearDuration).SetEase(m_ScaleAppearEase);
            damageHolder.Place(position);
        }

        public void Return(UIDamageHolder uiDamageHolder)
        {
            uiDamageHolder.transform.DoScale(Vector3.zero, m_ScaleDisappearDuration).SetEase(m_ScaleDisappearEase).OnComplete(() => m_DamageHolderPool.Return(uiDamageHolder)); 
        }
    }
}