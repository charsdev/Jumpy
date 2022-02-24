using UnityEngine;

public class RollerBackground : MonoBehaviour
{
    public float speed = 2f;
    public Material material;
    public Vector2 direction;

    void Update()
    {
        Vector2 offset = new Vector2( Time.time * speed,  Time.time * speed);
        material.mainTextureOffset = offset;
    }
}