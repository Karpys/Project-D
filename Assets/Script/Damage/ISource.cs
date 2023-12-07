using KarpysDev.Script.Behaviour;
using UnityEngine;

namespace KarpysDev.Script.Damage
{
    public interface ISource
    { 
        public IController Controller {get;}
        public Transform Root {get;}
        public Transform SpawnRoot {get;}
    }
}