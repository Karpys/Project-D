using System;
using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class BaseEntity : MonoBehaviour,IDamageReceiver
    {
        [Header("Entity Info")]
        [SerializeField] protected float m_AttackRange = 0f;
        [Header("References")]
        [SerializeField] private EntityAnimator m_Animator = null;
        [SerializeField] private Transform m_Root = null;
        [SerializeField] private EntityController m_BaseController = null;

        protected IController m_Controller = null;
        protected ISource m_Source = null;
        public EntityAnimator Animator => m_Animator;
        public Transform Root => m_Root;

        public Action OnInterupt = null;
        public IController Controller => m_Controller;

        protected virtual void Awake()
        {
            m_Controller = m_BaseController;
            m_Source = new EntitySource(this, m_Animator, m_Root);
        }

        public void ReceiveDamage(DamageSource damageSource,ISource source)
        {
            Debug.Log(gameObject.name + " lose hp : " + damageSource);

            if (source is EntitySource entitySource)
            {
                Debug.Log("Damage from: " + entitySource.Entity.name);
            }
        }

        public BaseEntity GetSource()
        {
            return this;
        }
    }

    public interface IController
    {
        void StopMovement();
        void SetTarget(ITargetable targetable);
        void SetLookAtTarget(Transform target);
    }
}