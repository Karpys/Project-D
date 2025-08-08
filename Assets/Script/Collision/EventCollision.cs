namespace KarpysDev.Script.Collision
{
    using System;
    using Player;
    using UnityEngine;
    using UnityEngine.Events;

    public class EventCollision : BaseCollisionAction
    {
        [SerializeField] private UnityEvent<ITargetable> m_Event = null;

        public override void AddCollisionAction(Action<ITargetable> action)
        {
            m_Event.AddListener(action.Invoke);
        }

        public override void Invoke(ITargetable targetable)
        {
            m_Event.Invoke(targetable);
        }
    }
}