using UnityEngine;

namespace Script
{
    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        [SerializeField] private LayerMask m_EnemyLayerMask;
        
        public LayerMask EnemyLayerMask => m_EnemyLayerMask;
    }
}