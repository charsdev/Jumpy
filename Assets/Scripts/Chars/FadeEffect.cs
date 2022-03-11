using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEventParams : EventArgs
{
    public float Duration { get; set; }

    public FadeEventParams(float duration)
    {
        Duration = duration;
    }
}

public class FadeEffect : MonoBehaviour
{
    private Image _fadeImage;
    private Color _initialColorImage;
    [SerializeField] private Color _fadeColor;
    [SerializeField] private bool fadeOnStart;

    private void Start()
    {
        _fadeImage = GetComponent<Image>();
        _initialColorImage = _fadeImage.color;
        Color color = Color.white;
        color.a = 0;
        _initialColorImage = color;

        if (fadeOnStart)
        {
            FadeOut(1);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("FadeOut",FadeOutHandler);
        EventManager.StartListening("FadeIn", FadeInHandler);
    }

  
    private void OnDisable()
    {
        EventManager.StopListening("FadeOut", FadeOutHandler);
        EventManager.StopListening("FadeIn", FadeInHandler);
    }


    public void FadeOutHandler(object sender, EventArgs eventParams)
    {
        Debug.Log($"Event: 'FadeOut'; Sender: {sender}; Receiver: {this}");

        FadeEventParams fadeEventParams = (FadeEventParams)eventParams;
        FadeOut(fadeEventParams.Duration);
    }

    public void FadeInHandler(object sender, EventArgs eventParams)
    {
        Debug.Log($"Event: 'FadeIn'; Sender: {sender}; Receiver: {this}");

        FadeEventParams fadeEventParams = (FadeEventParams)eventParams;
        FadeIn(fadeEventParams.Duration);
    }

    public IEnumerator Fade(Image fadeImage, float totalDuration, Color a, Color b)
    {
        float elapsedTime = 0f;
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(a, b, elapsedTime / totalDuration);
            yield return null;
        }
     
    }

    public void FadeOut(float totalDuration)
    {
        StartCoroutine(Fade(_fadeImage, totalDuration, _fadeColor, _initialColorImage));
    }

    public void FadeIn(float totalDuration)
    {
        StartCoroutine(Fade(_fadeImage, totalDuration, _initialColorImage, _fadeColor));
    }

}
