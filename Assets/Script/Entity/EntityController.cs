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

        protected int m_MovementLockCount = 0;
        protected Vector3 m_Destination = Vector3.zero;
        protected bool m_NeedToReachDestination = false;
        protected EntityCommand m_OverrideBehaviorCommand = null;

        protected ITargetable m_CurrentTargetable = null;
        protected Action A_OnNewCommand = null;
        public EntityAnimator Animator => m_EntityAnimator;
        public ITargetable Targetable => m_CurrentTargetable;
        public LookAt LookAt => m_LookAt;
        public Action OnNewCommand
        {
            get => A_OnNewCommand;
            set => A_OnNewCommand = value;
        }


        protected void Update()
        {
            EntityActionUpdate();

            LateUpdate();
        }

        protected virtual void EntityActionUpdate()
        {
            if (m_OverrideBehaviorCommand != null)
            {
                Debug.Log("execute override command");
                m_OverrideBehaviorCommand.Execute();
                return;
            }
                
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
            if(m_MovementLockCount > 0)
                return;
            
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
        
        public void MoveTowardsTarget()
        {
            if(m_MovementLockCount > 0)
                return;
            
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

        public void ChangeMovementLockCount(int count)
        {
            m_MovementLockCount += count;
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

        public void AddCommand(EntityCommand command)
        {
            m_OverrideBehaviorCommand = command;
        }

        public void ClearCommand()
        {
            m_OverrideBehaviorCommand = null;
        }
    }

    public abstract class EntityCommand
    {
        public abstract void Execute();
    }

    public class MoveTowardTargetableAndTryCastSpell : EntityCommand
    {
        private IController m_Controller = null;
        private Transform m_Caster = null;
        private ITargetable m_Target = null;
        private float m_AbilityRange = 0;
        private Ability m_Ability = null;
        
        public MoveTowardTargetableAndTryCastSpell(IController controller,Transform caster, ITargetable targetable, float range,Ability ability)
        {
            m_Ability = ability;
            m_Caster = caster;
            m_Controller = controller;
            m_Target = targetable;
            m_AbilityRange = range;

            controller.OnNewCommand += CancelCommand;
        }

        public override void Execute()
        {
            if (Vector3.Distance(m_Caster.position, m_Target.GetPivot.position) <= m_AbilityRange)
            {
                m_Ability.DirectCastAbility();
                CancelCommand();
            }
            else
            {
                m_Controller.MoveTowardsTarget();
            }
        }

        private void CancelCommand()
        {
            m_Controller.OnNewCommand -= CancelCommand;
            m_Controller.ClearCommand();
        }
    }
}