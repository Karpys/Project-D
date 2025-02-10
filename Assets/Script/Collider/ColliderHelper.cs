namespace  KarpysDev.Script.Collider
{
    using System;

    public static class ColliderHelper
    {
        public static void Check(this BaseCollider baseCollider, Action<BaseCollider> onCollision)
        {
            ColliderManager.CollisionCheck(baseCollider,onCollision);
        }
    }
}