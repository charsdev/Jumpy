using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    public void SetText(string newValue)
    {
        if (_textMesh == null) return;
        _textMesh.text = newValue;
    }

    public void SetText(string newValue, Color color)
    {
        if (_textMesh == null) return;
        _textMesh.text = newValue;
    }
}
