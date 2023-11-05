using KarpysDev.Script.Behaviour;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Damage
{
    public class EntitySource:ISource
    {
        private BaseEntity m_BaseEntity = null;
        private EntityAnimator m_Animator = null;
        private Transform m_Root = null;

        public BaseEntity Entity => m_BaseEntity;
        public EntityAnimator Animator => m_Animator;
        public Transform Root => m_Root;

        public EntitySource(BaseEntity entity, EntityAnimator animator, Transform root)
        {
            m_BaseEntity = entity;
            m_Animator = animator;
            m_Root = root;
        }
    }
}