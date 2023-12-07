using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Player
{
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