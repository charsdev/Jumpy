using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{
    private Image _fadeImage;
    private Color _initialColorImage;
    [SerializeField] private Color _fadeColor;
    [SerializeField] private float _duration;
    //This need to be refactor
    [SerializeField] private bool _fadeOutOnStart;
    [SerializeField] private bool _fadeInOnStart;
    public static FadeEffect Instance;
    public static bool FadeFinish;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _fadeImage = GetComponent<Image>();
        _initialColorImage = _fadeImage.color;
        if (_fadeInOnStart)
        {
            FadeIn(_duration);
        }
        else if (_fadeOutOnStart)
        {
            FadeOut(_duration);
        }
    }


    public static IEnumerator Fade(Image fadeImage, float totalDuration, Color a, Color b)
    {
        FadeFinish = false;
        float elapsedTime = 0f;
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(a, b, elapsedTime / totalDuration);
            yield return null;
        }

        if (elapsedTime >= totalDuration)
        {
            FadeFinish = true;
        }
    }

    public void FadeOut(float totalDuration)
    {
        StartCoroutine(Fade(_fadeImage, _duration, _fadeColor, _initialColorImage));
    }

    public void FadeIn(float totalDuration)
    {
        StartCoroutine(Fade(_fadeImage, _duration, _initialColorImage, _fadeColor));
    }

    public void ChangeScene(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    public void FadeInToScene(string level)
    {
        StartCoroutine(WaitFadeToChange(level));
    }


    private IEnumerator WaitFadeToChange( string level)
    {
        //yield return Fade(_fadeImage, _duration, _initialColorImage, _fadeColor);
        ChangeScene(level);
        yield return null;
    }
}
