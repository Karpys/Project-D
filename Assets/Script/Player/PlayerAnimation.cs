using System;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator = null;


        private string m_AnimationToPlay = String.Empty;

        public void AnimationCheck()
        {
            if(m_AnimationToPlay != String.Empty)
            {
                m_Animator.CrossFade(m_AnimationToPlay,0.05f);
                m_AnimationToPlay = String.Empty;
            }
        }
        
        public void PlayAnimation(string animationName)
        {
            m_AnimationToPlay = animationName;
        }
    }
}