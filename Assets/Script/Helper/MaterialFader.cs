namespace KarpysDev.Script.Helper
{
    using KarpysUtils;
    using KarpysUtils.TweenCustom;
    using UnityEngine;

    public class MaterialFader : MonoBehaviour
    {
        [SerializeField] private Renderer m_Renderer = null;
        [SerializeField] private float m_FadeTime = 1f;
        [SerializeField] private float m_FadeStartValue = 255f;
        [SerializeField] private float m_FadeEndValue = 0f;

        public void Start()
        {
            transform.DoFloatValue(f => m_Renderer.material.SetColor("_Color", m_Renderer.material.GetColor("_Color").setAlpha(f)), m_FadeStartValue, m_FadeEndValue, m_FadeTime);
        }
    }
}