using KarpysDev.Script.Damage;
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class EntityTargetableHolder : MonoBehaviour,IDamageTargetable
    {
        [SerializeField] private BaseEntity m_BaseEntity = null;

        public Transform GetPivot => m_BaseEntity.transform;
        public IDamageReceiver DamageReceiver => m_BaseEntity;
    }
}
