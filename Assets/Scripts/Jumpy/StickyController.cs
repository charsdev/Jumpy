using UnityEngine;

namespace Jumpy
{
    public class StickyController : MonoBehaviour
    {
        [SerializeField] private float _rollSpeed = 5;
        public CharacterBody CharacterBody;
        public Transform Model;

        private void Start() => CharacterBody = GetComponent<CharacterBody>();
        private void FixedUpdate() => CharacterBody.Rigidbody2D.AddForce(CharacterBody.GravityVector * CharacterBody.GravityModifier);

        private void Update()
        {
            if (CharacterBody.OnLeftWall)
            {
                CharacterBody.CurrentSide = CharacterBody.Side.LEFT;
            }
            else if (CharacterBody.OnGround)
            {
                CharacterBody.CurrentSide = CharacterBody.Side.DOWN;
            }
            else if (CharacterBody.OnRightWall)
            {
                CharacterBody.CurrentSide = CharacterBody.Side.RIGHT;
            }
            else if (CharacterBody.OnAbove)
            {
                CharacterBody.CurrentSide = CharacterBody.Side.UP;
            }

            if (JInput.JumpDown)
            {
                CharacterBody.Rigidbody2D.AddForce(CharacterBody.Up * 5f, ForceMode2D.Impulse);
            }

            if (JInput.HorizontalInput != 0)
            {
                CharacterBody.GravityModifier = CharacterBody.OnGround ? 0 : 1;
                transform.position += CharacterBody.Right * JInput.HorizontalInput * _rollSpeed * Time.deltaTime;
                //Model.transform.Rotate(-Vector3.forward * JInput.HorizontalInput, 500 * Time.deltaTime);
            }
            else
            {
                CharacterBody.GravityModifier = 1;
            }

            if (CharacterBody.OnGround && JInput.HorizontalInput == 0)
            {
               // Model.transform.rotation = Quaternion.Euler(0, 0, CharacterBody.angle);
            }
        }
    }
}
