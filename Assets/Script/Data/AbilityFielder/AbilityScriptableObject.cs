using KarpysDev.KarpysUtils.AutoFielder;
using UnityEngine;

namespace Script.Data.AbilityFielder
{
    using System;
    using KarpysDev.KarpysUtils;
    using KarpysDev.Script.Behaviour;
    using KarpysDev.Script.Damage;
    using Object = UnityEngine.Object;

    [CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability", order = 0)]
    public class AbilityScriptableObject : ScriptableObject,IFielder
    {
        [SerializeField] protected Fielder m_AbilityField = null;
        public Fielder Fielder => m_AbilityField;
        public Object TargetObject => this;

        public Ability CreateBaseAbility(ISource source,AbilityRule rule)
        {
            Type triggerClass = StringUtils.GetTypeViaClassName(m_AbilityField.ClassName);

            if (triggerClass == null || triggerClass.BaseType != typeof(Ability))
            {
                Debug.LogError("The ability class : " + m_AbilityField.ClassName + " is not recognized");
                return null;
            }
            
            object[] fieldValues = m_AbilityField.GetFields();
            object[] abilityConstructorFields = new object[fieldValues.Length + 2];
            abilityConstructorFields[0] = source;
            abilityConstructorFields[1] = rule;

            for (int i = 0; i < fieldValues.Length; i++)
            {
                abilityConstructorFields[i+2] = fieldValues[i];
            }

            foreach (object field in abilityConstructorFields)
            {
                field.GetType().Log("Type");
            }

            return (Ability)Activator.CreateInstance(triggerClass,abilityConstructorFields);
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