namespace KarpysDev.Script.Entity
{
    using System;
    using Damage;
    using UnityEngine;

    public interface IRoot
    {
        public Transform GetRoot(RootPosition rootPosition);
    }
}