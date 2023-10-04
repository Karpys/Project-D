using System;
using KarpysDev.Script.Behaviour;
using TweenCustom;
using UnityEditor.Animations;
using UnityEngine;

namespace KarpysDev.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform m_PlayerRoot = null;
        [SerializeField] private Camera m_PointCamera = null;
        [SerializeField] private PlayerAnimation m_PlayerAnimation = null;
        [SerializeField] private LookAt m_LookAt = null;
        
        [Header("Parameters")] 
        [SerializeField] private float m_Speed = 0;
        [SerializeField] private float m_DistanceToStop = 0;
        [SerializeField] private float m_RotationOffset = 0;
        [SerializeField] private LayerMask m_EnemyLayerMask;

        private Vector3 m_Destination = Vector3.zero;
        private bool m_NeedToReachDestination = false;

        private Transform m_TransformTarget = null;
        
        private void Update()
        {
            PlayerInput();

            PlayerActionUpdate();

            LateUpdate();
        }

        private void PlayerActionUpdate()
        {
            if (m_NeedToReachDestination)
            {
                if(!m_TransformTarget)
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
        }

        private bool TargetCheck(Ray point)
        {
            if (Physics.Raycast(point, out RaycastHit info, 1000, m_EnemyLayerMask))
            {
                ITargetable targetable = info.collider.gameObject.GetComponent<ITargetable>();
                Transform target = targetable.GetPivot;
                m_TransformTarget = target;
                m_NeedToReachDestination = true;
                m_PlayerAnimation.PlayAnimation("Walk");
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
                Transform playerRootTransform = m_PlayerRoot.transform;
                Vector3 playerPosition = playerRootTransform.position;
                Vector3 newPosition = new Vector3(info.point.x, playerPosition.y, info.point.z);
                m_Destination = newPosition;
                m_NeedToReachDestination = true;
                m_PlayerAnimation.PlayAnimation("Walk");
                    
                m_LookAt.SetPoint(m_Destination);
                m_LookAt.Active(true);
                
                //playerRootTransform.DoPivotRotate(angle + m_RotationOffset,0.2f);

                m_TransformTarget = null;
                m_LookAt.SetTarget(null);
            }
        }

        private void MoveTowardsDestination()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_Destination,m_Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Destination) <= m_DistanceToStop)
            {
                m_NeedToReachDestination = false;
                m_PlayerAnimation.PlayAnimation("Idle");
            }
            else
            {
                transform.position = newDestination;
            }
        }
        
        private void MoveTowardsTarget()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_TransformTarget.position,m_Speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, m_TransformTarget.position) <= m_DistanceToStop)
            {
                //m_NeedToReachDestination = false;
                m_PlayerAnimation.PlayAnimation("Idle");
            }
            else
            {
                transform.position = newDestination;
            }
        }
    }

    public interface ITargetable
    {
        Transform GetPivot { get; }
    }
    
}