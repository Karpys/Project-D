using System;
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
                    Vector3 newPosition = new Vector3(info.point.x, playerRootTransform.position.y, info.point.z);
                    m_Destination = newPosition;
                    m_NeedToReachDestination = true;
                    m_PlayerAnimation.PlayAnimation("Running");
                }
            }
        }

        private void MoveTowardsDestination()
        {
            Vector3 newDestination = Vector3.MoveTowards(m_PlayerRoot.position,m_Destination,m_Speed * Time.deltaTime);
            m_PlayerRoot.transform.position = newDestination;

            if (Vector3.Distance(m_PlayerRoot.transform.position, m_Destination) <= m_DistanceToStop)
            {
                m_NeedToReachDestination = false;
                m_PlayerAnimation.PlayAnimation("Idle");
            }
        }
    }
}