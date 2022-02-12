using UnityEngine;

namespace Jumpy
{
    public class JInput : MonoBehaviour
    {
        public static float HorizontalInput;
        public static bool JumpDown;
        public static bool JumpUp;
        public static bool JumpHeld;

        public void Update()
        {
            if (GameManager.Instance.CurrentStatus == GameManager.Status.GameInProgress && !GameManager.Instance.GameIsPaused)
            {
                HorizontalInput = Input.GetAxisRaw("Horizontal");
                JumpDown = Input.GetButtonDown("Jump");
                JumpHeld = Input.GetButton("Jump");
                JumpUp = Input.GetButtonUp("Jump");
            }
        }
    }

}
