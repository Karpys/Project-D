namespace Script.Data.AbilityFielder
{
    using System;
    using UnityEngine;

    public class Tester : MonoBehaviour
    {
        [SerializeField] private AbilityScriptableObject m_ScriptableObject = null;

        private CustomClass customClass = null;

        private void Awake()
        {
            customClass = m_ScriptableObject.CreateInstance() as CustomClass;
        }
    }
}