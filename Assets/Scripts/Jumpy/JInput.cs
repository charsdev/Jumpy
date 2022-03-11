using UnityEngine;

namespace Jumpy
{
    public class JInput : MonoBehaviour
    {
        public static float HorizontalInput;
        public static bool JumpDown;
        public static bool JumpUp;
        public static bool JumpHeld;
        public static bool PowerJumpDown;
        public static bool PowerJumpUp;
        public static bool PowerJumpUpHeld;
        public static bool MouseMoving;
        public void Update()
        {
            if (GameManager.Instance.CurrentStatus == GameManager.Status.GameInProgress && !GameManager.Instance.GameIsPaused)
            {
                HorizontalInput = Input.GetAxisRaw("Horizontal");
                JumpDown = Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Fire 1 Joystick"); //|| Input.GetKeyDown(KeyCode.UpArrow);
                JumpHeld = Input.GetKey(KeyCode.W) || Input.GetButton("Fire 1 Joystick");//|| Input.GetKey(KeyCode.UpArrow); 
                JumpUp = Input.GetKeyUp(KeyCode.W) || Input.GetButtonUp("Fire 1 Joystick"); // || Input.GetKeyUp(KeyCode.UpArrow);
                PowerJumpDown = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4);
                PowerJumpUp = Input.GetKeyUp(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton4);
                PowerJumpUpHeld = Input.GetKey(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton4);
            }
        }
    }

}
