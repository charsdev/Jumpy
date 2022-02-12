using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Chars
{
    public class LevelBackground : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void LateUpdate()
        {
            transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, transform.position.z);
        }
    }
}