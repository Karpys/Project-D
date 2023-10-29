using System;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class BaseEntity : MonoBehaviour
    {
        [SerializeField] private EntityAnimator m_Animator = null;
        [SerializeField] private Transform m_Root = null;

        public EntityAnimator Animator => m_Animator;
        public Transform Root => m_Root;

        public Action OnInterupt = null;
    }
}