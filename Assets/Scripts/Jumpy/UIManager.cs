using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class GraphText
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textGUI;
    public int Value;
    public Animator[] _animators = new Animator[2];

    public GraphText(Image image, TextMeshProUGUI text, int value)
    {
        _image = image;
        _textGUI = text;
        Value = value;
    }

    public void SetText(string value) => _textGUI.text = value;
    public void SetTrigger(string trigger)
    {
        foreach (var item in _animators)
        {
            item.SetTrigger(trigger);
        }
    }
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GraphText Score;
    public void Awake() => instance = this;
    private void Update() => Score.SetText(Score.Value.ToString() + '/' + 8);
}
