namespace Script
{
    using System;
    using KarpysDev.KarpysUtils;

    public class UpdateManager : SingletonMonoBehavior<UpdateManager>
    {
        public Action OnUpdate = null;

        public void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}