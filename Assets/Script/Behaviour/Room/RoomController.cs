namespace KarpysDev.Script.Behaviour.Room
{
    using UnityEngine;

    public class RoomController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator = null;
        
        public void TryOpenRoom()
        {
            OpenRoom();
        }
        
        public void TryCloseRoom()
        {
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