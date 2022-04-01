using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackgroundMusic : MonoBehaviour
{
    private void Start()
    {
        AudioClip clip = AudioManager.Instance.GetAudioClip("MenuIntro");
        AudioManager.Instance.PlayAudio(clip);
    }
}
