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
    public class  AbilityScriptableObject : ScriptableObject,IFielder
    {
        [SerializeField] protected Fielder m_AbilityField = null;
        public Fielder Fielder => m_AbilityField;
        public Object TargetObject => this;

        private object[] m_FieldValues = null;
        
        public void GenerateFields()
        {
            m_FieldValues = m_AbilityField.GetFields();
        }

        public Ability CreateBaseAbility(ISource source,AbilityRule rule, AbilityRestriction abilityRestriction)
        {
            GenerateFields();
            Type triggerClass = StringUtils.GetTypeViaClassName(m_AbilityField.ClassName);

            if (triggerClass == null || !triggerClass.IsSubclassOf(typeof(Ability)))
            {
                Debug.LogError("The ability class : " + m_AbilityField.ClassName + " is not recognized");
                return null;
            }

            object[] abilityConstructorFields = new object[m_FieldValues.Length + 3];
            abilityConstructorFields[0] = source;
            abilityConstructorFields[1] = rule;
            abilityConstructorFields[2] = abilityRestriction;

            for (int i = 0; i < m_FieldValues.Length; i++)
            {
                abilityConstructorFields[i+3] = m_FieldValues[i];
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