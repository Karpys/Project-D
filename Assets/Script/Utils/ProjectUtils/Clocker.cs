using UnityEngine;

namespace KarpysDev.Script.Utils.ProjectUtils
{
    public class Clocker
    {
        private float m_Delay = 0f;
        private float m_Timer = 0f;

        public bool IsReady => m_Timer <= 0;
        
        public Clocker(float delay)
        {
            m_Delay = delay;
        }

        public void UpdateClock()
        {
            if(m_Timer > 0)
                m_Timer -= Time.deltaTime;
        }

        public void Clear()
        {
            m_Timer = -1;
        }

        public void Set(float delay)
        {
            m_Delay = delay;
        }

        public void Launch()
        {
            m_Timer = m_Delay;
        }
    }
}
