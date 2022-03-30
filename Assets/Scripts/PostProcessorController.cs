using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessorController : MonoBehaviour
{
    public PostProcessVolume CurrentVolume;
    private Bloom _bloom;
    private Vignette _vignette;

    private void Start()
    {
        if (CurrentVolume != null)
        {
            CurrentVolume.profile.TryGetSettings(out _bloom);
            CurrentVolume.profile.TryGetSettings(out _vignette);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            _bloom.intensity.value += Time.deltaTime;
            _vignette.intensity.value += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            _bloom.intensity.value -= Time.deltaTime;
            _vignette.intensity.value -= Time.deltaTime;
        }
    }

    private void OnValidate()
    {
        if (CurrentVolume)
        {
            CurrentVolume.profile.TryGetSettings(out _bloom);
            CurrentVolume.profile.TryGetSettings(out _vignette);
        }
    }
}
