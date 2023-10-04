
using KarpysDev.Script.Player;
using UnityEngine;

namespace KarpysDev.Script.Behaviour
{
    public class TargetableHolder : MonoBehaviour,ITargetable
    {
        [SerializeField] private Transform m_Pivot = null;

        public Transform GetPivot => m_Pivot;
    }
}
