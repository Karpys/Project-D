using Script;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    //Only for player => Make this abstact with get groundPosition
    //=> for player raycast from main camera and for enemy direct info from player info
    public class GroundCastAbilityRule : AbilityRule
    {
        private Vector3 m_GroundHitPosition = Vector3.zero;
        private bool m_HasHit = false;

        public Vector3 GroundHitPosition => m_GroundHitPosition;
        public override bool IsCompelte()
        {
            return m_HasHit;
        }

        public override void GetInfo()
        {
            m_HasHit = false;
            Ray point = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(point, out RaycastHit info, 100, GameManager.Instance.GroundLayerMask))
            {
                m_HasHit = true;
                m_GroundHitPosition = info.point;
            }
        }
    }
}