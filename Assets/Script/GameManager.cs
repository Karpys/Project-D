using KarpysDev.KarpysUtils;
using UnityEngine;

namespace Script
{
    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        [SerializeField] private LayerMask m_EnemyLayerMask;
        [SerializeField] private LayerMask m_GroundLayerMask;
        
        public LayerMask EnemyLayerMask => m_EnemyLayerMask;
        public LayerMask GroundLayerMask => m_GroundLayerMask;
    }
}