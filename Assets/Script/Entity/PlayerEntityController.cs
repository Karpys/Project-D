using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class PlayerEntityController : EntityController
    {
        [SerializeField] private PlayerEntity m_PlayerEntity = null;
        [SerializeField] private Camera m_PointCamera = null;
       

        protected override void EntityActionUpdate()
        {
            PlayerInput();

            base.EntityActionUpdate();
        }

        private void PlayerInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray point = m_PointCamera.ScreenPointToRay(Input.mousePosition);
                if (!TargetCheck(point))
                {
                    GroundCheckMovement(point);
                };
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                m_PlayerEntity.SpinAuto.CastAbility();
            }
        }

        private bool TargetCheck(Ray point)
        {
            if (Physics.Raycast(point, out RaycastHit info, 1000, m_EnemyLayerMask))
            {
                ITargetable targetable = info.collider.gameObject.GetComponent<ITargetable>();
                Transform target = targetable.GetPivot;

                if (target == m_TransformTarget)
                    return true;
                
                m_PlayerEntity.OnInterupt?.Invoke();
                m_TransformTarget = target;
                m_NeedToReachDestination = true;
                m_PlayerAnimation.PlayBotAnimation("Walk");
                m_LookAt.SetTarget(target);
                m_LookAt.Active(true);
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
                m_PlayerEntity.OnInterupt?.Invoke();
                m_Destination = newPosition;
                m_NeedToReachDestination = true;
                m_PlayerAnimation.PlayBotAnimation("Walk");
                    
                m_LookAt.SetPoint(m_Destination);
                m_LookAt.Active(true);
                
                //playerRootTransform.DoPivotRotate(angle + m_RotationOffset,0.2f);

                m_TransformTarget = null;
                m_LookAt.SetTarget(null);
            }
        }
        
        protected override void OnTargetReached()
        {
            base.OnTargetReached();
            
            TryLaunchAutoAttack();
        }
        
        private void TryLaunchAutoAttack()
        {
            m_PlayerEntity.AutoAttack.CastAbility();
        }
    }
}