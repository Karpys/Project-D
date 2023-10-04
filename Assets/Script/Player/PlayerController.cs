using System;
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
        
        [Header("Parameters")] 
        [SerializeField] private float m_Speed = 0;
        [SerializeField] private float m_DistanceToStop = 0;
        [SerializeField] private float m_RotationOffset = 0;

        private Vector3 m_Destination = Vector3.zero;
        private bool m_NeedToReachDestination = false;
        
        private void Update()
        {
            MovementInput();

            if(m_NeedToReachDestination)
                MoveTowardsDestination();

            LateUpdate();
        }

        private void LateUpdate()
        {
            m_PlayerAnimation.AnimationCheck();
        }
        
        private void MovementInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray point = m_PointCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(point, out RaycastHit info))
                {
                    Transform playerRootTransform = m_PlayerRoot.transform;
                    Vector3 playerPosition = playerRootTransform.position;
                    Vector3 newPosition = new Vector3(info.point.x, playerPosition.y, info.point.z);
                    m_Destination = newPosition;
                    m_NeedToReachDestination = true;
                    m_PlayerAnimation.PlayAnimation("Walk");
                    
                    Vector3 direction = playerPosition - m_Destination;
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    playerRootTransform.DoPivotRotate(angle + m_RotationOffset,0.2f);
                }
            }
        }

        private void MoveTowardsDestination()
        {
            Vector3 newDestination = Vector3.MoveTowards(transform.position,m_Destination,m_Speed * Time.deltaTime);
            transform.position = newDestination;

            if (Vector3.Distance(transform.position, m_Destination) <= m_DistanceToStop)
            {
                m_NeedToReachDestination = false;
                m_PlayerAnimation.PlayAnimation("Idle");
            }
        }
    }
}