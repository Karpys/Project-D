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
        private Transform m_SpawnRoot = null;
        private EntityController m_Controller = null;

        public BaseEntity Entity => m_BaseEntity;
        public EntityAnimator Animator => m_Animator;
        public Transform Root => m_Root;
        public Transform SpawnRoot => m_SpawnRoot;
        public EntityController EntityController => m_Controller;
        public IController Controller => m_Controller;

        public EntitySource(BaseEntity entity, EntityAnimator animator, Transform root,Transform spawnRoot,EntityController entityController)
        {
            m_BaseEntity = entity;
            m_Animator = animator;
            m_Root = root;
            m_Controller = entityController;
            m_SpawnRoot = spawnRoot;
        }

    }
}