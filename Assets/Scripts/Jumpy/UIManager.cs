using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chars.Tools;
using System;

[Serializable]
public class GraphText
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textGUI;
    public Animator[] _animators = new Animator[2];

    public GraphText(Image image, TextMeshProUGUI text)
    {
        _image = image;
        _textGUI = text;
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

public class UIManager : Singleton<UIManager>
{
    public GraphText Score;
    public string ScorePattern;
 
    public void RefreshPoints()
    {
        Score.SetText(GameManager.Instance.Score.ToString(ScorePattern + GameManager.instance.ScoreThreashold));
        Score.SetTrigger("Collect");
    }

}
