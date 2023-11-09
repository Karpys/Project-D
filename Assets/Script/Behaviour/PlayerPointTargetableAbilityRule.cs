using KarpysDev.Script.Player;
using Script;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    //Made a super class PointTargetatbleSpellRule//
    public class PlayerPointTargetableAbilityRule:AbilityRule
    {
        private ITargetable m_Targetable = null;
        private float m_Range = 0;
        private Transform m_Caster = null;
        private IController m_Controller = null;
        public ITargetable Targetable => m_Targetable;

        private bool m_InRangeAndTarget = false;

        public PlayerPointTargetableAbilityRule(Transform caster, float range)
        {
            m_Range = range;
            m_Caster = caster;
        }

        public PlayerPointTargetableAbilityRule(Transform caster, float range,IController controller)
        {
            m_Range = range;
            m_Caster = caster;
            m_Controller = controller;
        }
        public override void GetInfo()
        {
            //Add Camera in constructor or acces through a CameraManager
            Ray point = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(point, out RaycastHit info,100, GameManager.Instance.EnemyLayerMask))
            {
                ITargetable targetable = info.collider.gameObject.GetComponent<ITargetable>();

                if(targetable == null)
                    return;
                
                if (Vector3.Distance(m_Caster.position, targetable.GetPivot.position) <= m_Range)
                {
                    m_Targetable = targetable;
                    m_InRangeAndTarget = true;
                    return;
                }else if (m_Controller != null)
                {
                    m_Targetable = targetable;
                    m_Controller.SetTarget(targetable);
                    OnRangeFail();
                    return;
                }
            }
            
            m_Targetable = null;
            m_InRangeAndTarget = false;
        }

        public override bool IsCompelte()
        {
            return m_InRangeAndTarget;
        }

        private void OnRangeFail()
        {
            m_Controller.AddCommand(new MoveTowardTargetableAndTryCastSpell(m_Controller,m_Caster,m_Targetable,m_Range,m_Ability));
        }
    }
}