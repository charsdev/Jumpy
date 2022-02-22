using UnityEngine;
using UnityEngine.UI;

namespace Chars.Tools
{
	public class DiagnalParallax : MonoBehaviour
	{
		private Material material;
		public float speed = 50f;
		public Vector3 direction;

        private void Start()
        {
			material = GetComponent<Image>().material;
			material.shader = Shader.Find("Custom/CG_Offset");
		}

        private void Update()
        {
			if (Application.isPlaying ) 
				UpdateShader();
			if (!Application.isPlaying)
				material?.SetFloat("_Speed", 0);
		}

		private void UpdateShader()
		{
			material.SetFloat("_Horizontal", direction.x);
			material.SetFloat("_Vertical", direction.y);
			material.SetFloat("_Speed", speed);
		}

		public void SetDirection(Vector3 dir)
		{
			direction = dir;
		}

        private void OnDestroy()
        {
			material.SetFloat("_Speed", 0);
		}
	}
}
