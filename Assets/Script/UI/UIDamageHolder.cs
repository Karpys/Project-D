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
            Invoke("Return",CanvasDamage.RETURN_TEXT_DURATION);
        }

        public void Place(Vector3 position)
        {
            transform.position = position;
        }

        public void Return()
        {
            CanvasDamage.Instance.Return(this);
        }
    }
}