namespace KarpysDev.Script.UI
{
    using KarpysDev.Script.Damage;
    using KarpysDev.Script.Utils.ProjectUtils;
    using TMPro;
    using UnityEngine;

    public class UIDamageHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText = null;

        public void Initialize(DamageSource damageSource)
        {
            _damageText.text = "" + damageSource.Damage;
            _damageText.color = ColorLibrary.Instance.GetDamageColor(damageSource.DamageType);
            Invoke("Return",1);
        }

        public void Place(Transform origin)
        {
            transform.position = Camera.main.WorldToScreenPoint(origin.position);
        }

        public void Return()
        {
            CanvasDamage.Instance.Return(this);
        }
    }
}