using System;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class EntityAnimator : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator = null;
        
        private string m_TopAnimationToPlay = String.Empty;
        private string m_CurrentTopAnimation = String.Empty;
        private float m_TopTransitionDuration = 0.05f;

        private string m_BotAnimationToPlay = String.Empty;
        private string m_CurrentBotAnimation = String.Empty;
        private float m_BotTransitionDuration = 0.05f;

        public void AnimationCheck()
        {
            if(m_TopAnimationToPlay != String.Empty)
            {
                m_Animator.CrossFadeInFixedTime(m_TopAnimationToPlay,m_TopTransitionDuration);
                m_CurrentTopAnimation = m_TopAnimationToPlay;
                m_TopAnimationToPlay = String.Empty;
            }
            
            if(m_BotAnimationToPlay != String.Empty)
            {
                m_Animator.CrossFadeInFixedTime(m_BotAnimationToPlay,m_BotTransitionDuration);
                m_CurrentBotAnimation = m_BotAnimationToPlay;
                m_BotAnimationToPlay = String.Empty;
            }
        }

        public void PlayTopAnimation(string animationName, float duration = 0.05f, bool replay = true)
        {
            Debug.Log("Animation to play: " + animationName);
            
            
            m_TopAnimationToPlay = animationName;
            m_TopTransitionDuration = duration;
        }

        public void PlayOrContinueTopAnimation(string animationName, float duration = 0.05f)
        {
            if(m_CurrentTopAnimation == animationName)
                return;
            PlayTopAnimation(animationName,duration);
        }
        
        public void PlayBotAnimation(string animationName,float duration = 0.05f)
        {
            Debug.Log("Animation to play: " + animationName);
            m_BotAnimationToPlay = animationName;
            m_BotTransitionDuration = duration;
        }
        
        public void PlayOrContinueBotAnimation(string animationName, float duration = 0.05f)
        {
            if(m_CurrentBotAnimation == animationName)
                return;
            PlayBotAnimation(animationName,duration);
        }
    }
}