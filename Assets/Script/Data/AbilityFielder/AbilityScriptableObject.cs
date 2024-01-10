using KarpysDev.KarpysUtils.AutoFielder;
using UnityEngine;

namespace Script.Data.AbilityFielder
{
    using System;
    using KarpysDev.KarpysUtils;
    using KarpysDev.Script.Damage;
    using Object = UnityEngine.Object;

    [CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability", order = 0)]
    public class AbilityScriptableObject : ScriptableObject,IFielder
    {
        [SerializeField] protected Fielder m_AbilityField = null;
        [SerializeField] protected int m_MyNewField = 0;
        [SerializeField] protected int m_MyNewField2= 0;
        public Fielder Fielder => m_AbilityField;
        public Object TargetObject => this;

        public object CreateInstance()
        {
            object[] fieldValues = m_AbilityField.GetFields();

            Type triggerClass = StringUtils.GetTypeViaClassName(m_AbilityField.ClassName);

            if (triggerClass == null)
            {
                Debug.LogError("The class : " + m_AbilityField.ClassName + " is not recognized");
                return null;
            }
        
            return Activator.CreateInstance(triggerClass,fieldValues);
        }
    }

    public class CustomClass
    {
        public GameObject Prefab;
        public CustomClass(GameObject gameObject)
        {
            Prefab = gameObject;
        }
    }
}