namespace Script.Fx
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class FxManager : SingletonMonoBehavior<FxManager>
    {
        [SerializeField] private TormentPulseFx m_TormentPulseFxPrefab = null;

        public void CreateTormentPulseFx(Transform a, Transform b)
        {
            TormentPulseFx tormentPulseFx = Instantiate(m_TormentPulseFxPrefab, Vector3.zero, Quaternion.identity, transform);
            tormentPulseFx.Initialize(a,b);
        }
    }
}