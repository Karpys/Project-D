namespace KarpysDev.Script.Behaviour.Room
{
    using Collision;
    using Entity;
    using Player;
    using UnityEngine;

    public class RoomController : MonoBehaviour
    {
        [SerializeField] private BaseCollision m_FrontGateCollision = null;
        [SerializeField] private Animator m_Animator = null;
        
        public void TryOpenRoom(ITargetable contact)
        {
            if(contact.Source.Root.GetRoot(RootPosition.Root).CompareTag("Player"))
                OpenRoom();
        }
        
        public void TryCloseRoom(ITargetable contact)
        {
            if(contact.Source.Root.GetRoot(RootPosition.Root).CompareTag("Player"))
                CloseRoom();
        }

        private void OpenRoom()
        {
            m_Animator.Play("Open");
            
        }

        private void CloseRoom()
        {
            m_Animator.Play("Close");
        }
    }
}