namespace Script.Data.AbilityFielder
{
    using System;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class Tester : MonoBehaviour
    {
        [SerializeField] private AbilityScriptableObject m_ScriptableObject = null;

        public void Awake()
        {
            CustomClass obj = m_ScriptableObject.CreateInstance() as CustomClass;
            obj.Key.Log("KeyCode");
        }
    }
}