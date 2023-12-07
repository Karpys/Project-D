using System;
using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class PlayerEntityController : EntityController
    {
        [SerializeField] private PlayerEntity m_PlayerEntity = null;
        [SerializeField] private Camera m_PointCamera = null;
        [SerializeField] protected LayerMask m_EnemyLayerMask;


        private string m_LastCommandId = String.Empty;
        protected override void EntityActionUpdate()
        {
            PlayerInput();
            base.EntityActionUpdate();
        }

        private void TriggerCommand(string newCommandId)
        {
            if(newCommandId != m_LastCommandId)
                OnNewCommand?.Invoke();
            m_LastCommandId = newCommandId;
        }
        private void PlayerInput()
        {
            if (Input.GetMouseButton(1))
            {
                TriggerCommand("Movement");
                Ray point = m_PointCamera.ScreenPointToRay(Input.mousePosition);
                if (!TargetCheck(point))
                {
                    GroundCheckMovement(point);
                };
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                TriggerCommand("Ability1");
                m_PlayerEntity.SpinAuto.CastAbility();
            }else if (Input.GetKeyDown(KeyCode.Z))
            {
                TriggerCommand("Ability2");
                m_PlayerEntity.AutoAttack.CastAbility();
            }else if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerCommand("Ability3");
                m_PlayerEntity.Projectile.CastAbility();
            }
        }
        
        private bool TargetCheck(Ray point)
        {
            if (Physics.Raycast(point, out RaycastHit info, 1000, m_EnemyLayerMask))
            {
                ITargetable targetable = info.collider.gameObject.GetComponent<ITargetable>();

                if (targetable == m_CurrentTargetable)
                    return true;
                
                SetTarget(targetable);
                return true;
            }

            return false;
        }

        private void GroundCheckMovement(Ray point)
        {
            if (Physics.Raycast(point, out RaycastHit info))
            {
                Transform playerRootTransform = m_EntityRoot.transform;
                Vector3 playerPosition = playerRootTransform.position;
                Vector3 newPosition = new Vector3(info.point.x, playerPosition.y, info.point.z);
                m_Destination = newPosition;
                m_NeedToReachDestination = true;
                m_EntityAnimator.PlayOrContinueBotAnimation("Walk");
                    
                m_LookAt.SetPoint(m_Destination);
                m_LookAt.Active(true);
                
                //playerRootTransform.DoPivotRotate(angle + m_RotationOffset,0.2f);
                m_CurrentTargetable = null;
                m_LookAt.SetTarget(null);
            }
        }

        public override void SetTarget(ITargetable targetable)
        {
            base.SetTarget(targetable);
            m_LookAt.SetTarget(targetable.GetPivot);
            m_LookAt.Active(true);
            m_NeedToReachDestination = true;
            m_EntityAnimator.PlayOrContinueBotAnimation("Walk");
        }
        
        protected override void OnTargetReached()
        {
            base.OnTargetReached();

            m_EntityAnimator.PlayOrContinueBotAnimation("Idle",0.25f);
            return;
            TryLaunchAutoAttack();
        }
        
        private void TryLaunchAutoAttack()
        {
            m_PlayerEntity.AutoAttack.CastAbility();
        }
    }
}