namespace KarpysDev.Script.Collision
{
    using System;
    using Player;

    public class ManualCollisionAction : BaseCollisionAction
    {
        private Action<ITargetable> A_OnCollisionDetected = null;

        public override void Invoke(ITargetable targetable)
        {
            A_OnCollisionDetected.Invoke(targetable);
        }

        public override void AddCollisionAction(Action<ITargetable> action)
        {
            A_OnCollisionDetected += action;
        }
    }
}