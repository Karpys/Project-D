using System;
using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class BaseEntity : MonoBehaviour,IDamageReceiver
    {
        [SerializeField] private EntityAnimator m_Animator = null;
        [SerializeField] private Transform m_Root = null;

        protected ISource m_Source = null;
        public EntityAnimator Animator => m_Animator;
        public Transform Root => m_Root;

        public Action OnInterupt = null;

        protected virtual void Awake()
        {
            m_Source = new EntitySource(this, m_Animator, m_Root);
        }

        public void ReceiveDamage(DamageSource damageSource,ISource source)
        {
            Debug.Log("Entity lose hp :" + damageSource);

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
}