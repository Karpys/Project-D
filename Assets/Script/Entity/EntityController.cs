using System;
using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class EntityController : MonoBehaviour,IController
    {
        [Header("References")] 
        [SerializeField] protected Transform m_EntityRoot = null;
        [SerializeField] protected EntityAnimator m_EntityAnimator = null;
        [SerializeField] protected LookAt m_LookAt = null;
        
        [Header("Entity Parameters")] 
        [SerializeField] protected float m_Speed = 0;
        [SerializeField] protected float m_DistanceToStop = 0;
        [SerializeField] protected float m_TargetRangeStop = 0;

        protected Vector3 m_Destination = Vector3.zero;
        protected bool m_NeedToReachDestination = false;

        protected ITargetable m_CurrentTargetable = null;

        public EntityAnimator Animator => m_EntityAnimator;
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
            m_EntityAnimator.AnimationCheck();
        }
        

        private void MoveTowardsDestination()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_Destination,m_Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Destination) <= m_DistanceToStop)
            {
                m_NeedToReachDestination = false;
                m_EntityAnimator.PlayBotAnimation("Idle");
            }
            else
            {
                transform.position = newDestination;
            }
        }
        
        private void MoveTowardsTarget()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_CurrentTargetable.GetPivot.position,m_Speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, m_CurrentTargetable.GetPivot.position) <= m_TargetRangeStop)
            {
                OnTargetReached();
            }
            else
            {
                transform.position = newDestination;
            }
        }

        protected virtual void OnTargetReached(){}
        public void StopMovement()
        {
            m_NeedToReachDestination = false;
            m_Destination = transform.position;
            m_EntityAnimator.PlayOrContinueBotAnimation("Idle",.25f);
        }

        public virtual void SetTarget(ITargetable targetable)
        {
            m_CurrentTargetable = targetable;
        }

        public void SetLookAtTarget(Transform target)
        {
            m_LookAt.SetTarget(target);
        }
    }
}