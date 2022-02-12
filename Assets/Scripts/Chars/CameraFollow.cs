using UnityEngine;

namespace Chars.Tools
{
    public class CameraFollow : MonoBehaviour
    {
        private BoxCollider2D _cameraBox;
        private Transform _player;
        public BoxCollider2D _boundary;
        public Vector2 _min, _max;

        private void OnEnable()
        {
        }

        private void Start()
        {
            _boundary = GetComponentInParent<BoxCollider2D>();
            _cameraBox = gameObject.AddComponent<BoxCollider2D>();
            _cameraBox.isTrigger = true;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        private void Update()
        {
            CalculateCameraBound();
            FollowPlayer();
        }

        private void CalculateCameraBound()
        {
            var xRatio = (float)Screen.width / (float)Screen.height;
            var yRatio = (float)Screen.height / (float)Screen.width;

            var boxSizeX = (Mathf.Abs(Camera.main.orthographicSize) * 2 * xRatio);
            var boxSizeY = boxSizeX * yRatio;

            _cameraBox.size = new Vector2(boxSizeX, boxSizeY);
        }

        private void FollowPlayer()
        {
            _min = (Vector2)_boundary.bounds.min + _cameraBox.size / 2;
            _max = (Vector2)_boundary.bounds.max - _cameraBox.size / 2;

            float clampX = Mathf.Clamp(_player.position.x, _min.x, _max.x);
            float clampY = Mathf.Clamp(_player.position.y, _min.y, _max.y);
            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }

    }
}