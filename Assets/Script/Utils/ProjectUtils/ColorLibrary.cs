namespace KarpysDev.Script.Utils.ProjectUtils
{
    using System.Collections.Generic;
    using Damage;
    using KarpysUtils;
    using UnityEngine;

    [System.Serializable]
    public class DamageTypeColor
    {
        public DamageType DamageType = DamageType.Physical;
        public Color DamageColor = Color.white; 
    }
    
    public class ColorLibrary : SingletonMonoBehavior<ColorLibrary>
    {
        [SerializeField] private DamageTypeColor[] m_DamageTypeColors = null;

        private Dictionary<DamageType, Color> m_DamageColorLibrary = null;
        private void Awake()
        {
            m_DamageColorLibrary = new Dictionary<DamageType, Color>();
            
            foreach (DamageTypeColor damageTypeColor in m_DamageTypeColors)
            {
                if(m_DamageColorLibrary.ContainsKey(damageTypeColor.DamageType))
                    continue;
                m_DamageColorLibrary.Add(damageTypeColor.DamageType,damageTypeColor.DamageColor);
            }
        }

        public Color GetDamageColor(DamageType damageType)
        {
            if (m_DamageColorLibrary.TryGetValue(damageType, out Color color))
                return color;

            return Color.white;
        }
    }
}