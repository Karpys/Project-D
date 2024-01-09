using KarpysDev.KarpysUtils.AutoFielder;
using UnityEngine;

namespace Script.Data.AbilityFielder
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability", order = 0)]
    public class AbilityScriptableObject : ScriptableObject,IFielder
    {
        [SerializeField] protected Fielder m_AbilityField = null;
        [SerializeField] protected int m_MyNewField = 0;
        [SerializeField] protected int m_MyNewField2= 0;
        public Fielder Fielder => m_AbilityField;
        public Object TargetObject => this;
    }
}