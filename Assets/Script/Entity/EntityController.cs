using System;
using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class EntityController : MonoBehaviour,ITargetProvider
    {
        [Header("References")] 
        [SerializeField] protected Transform m_EntityRoot = null;
        [SerializeField] protected EntityAnimator m_PlayerAnimation = null;
        [SerializeField] protected LookAt m_LookAt = null;
        
        [Header("Entity Parameters")] 
        [SerializeField] protected float m_Speed = 0;
        [SerializeField] protected float m_DistanceToStop = 0;
        [SerializeField] protected float m_AttackRange = 0;
        [SerializeField] protected LayerMask m_EnemyLayerMask;

        protected Vector3 m_Destination = Vector3.zero;
        protected bool m_NeedToReachDestination = false;

        protected ITargetable m_CurrentTargetable = null;

        public EntityAnimator Animator => m_PlayerAnimation;
        public ITargetable Targetable => m_CurrentTargetable;


        protected void Update()
        {
            EntityActionUpdate();

            LateUpdate();
        }

        protected virtual void EntityActionUpdate()
        {
            if (m_NeedToReachDestination)
            {
                if(m_CurrentTargetable == null)
                    MoveTowardsDestination();
                else
                {
                    MoveTowardsTarget();
                }
            }
        }

        private void LateUpdate()
        {
            m_PlayerAnimation.AnimationCheck();
        }
        

        private void MoveTowardsDestination()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_Destination,m_Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Destination) <= m_DistanceToStop)
            {
                m_NeedToReachDestination = false;
                m_PlayerAnimation.PlayBotAnimation("Idle");
            }
            else
            {
                transform.position = newDestination;
            }
        }
        
        private void MoveTowardsTarget()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_CurrentTargetable.GetPivot.position,m_Speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, m_CurrentTargetable.GetPivot.position) <= m_AttackRange)
            {
                OnTargetReached();
            }
            else
            {
                transform.position = newDestination;
            }
        }

        protected virtual void OnTargetReached(){}
    }
}