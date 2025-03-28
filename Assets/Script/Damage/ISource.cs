using KarpysDev.Script.Behaviour;

namespace KarpysDev.Script.Damage
{
    using Entity;

    public interface ISource
    { 
        public IController Controller {get;}
        //Encapsulate Root / Spawn Root in an interface with enum call
        public IRoot Root { get; }
    }
}