using System;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator = null;
        
        private string m_TopAnimationToPlay = String.Empty;
        private float m_TopTransitionDuration = 0.05f;
        
        private string m_BotAnimationToPlay = String.Empty;
        private float m_BotTransitionDuration = 0.05f;

        public void AnimationCheck()
        {
            if(m_TopAnimationToPlay != String.Empty)
            {
                m_Animator.CrossFadeInFixedTime(m_TopAnimationToPlay,m_TopTransitionDuration);
                m_TopAnimationToPlay = String.Empty;
            }
            
            if(m_BotAnimationToPlay != String.Empty)
            {
                m_Animator.CrossFadeInFixedTime(m_BotAnimationToPlay,m_BotTransitionDuration);
                m_BotAnimationToPlay = String.Empty;
            }
        }
        
        public void PlayTopAnimation(string animationName,float duration = 0.05f)
        {
            Debug.Log("Animation to play: " + animationName);
            m_TopAnimationToPlay = animationName;
            m_TopTransitionDuration = duration;
        }
        
        public void PlayBotAnimation(string animationName,float duration = 0.05f)
        {
            Debug.Log("Animation to play: " + animationName);
            m_BotAnimationToPlay = animationName;
            m_BotTransitionDuration = duration;
        }
    }
}