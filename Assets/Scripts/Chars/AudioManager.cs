using System.Collections.Generic;
using UnityEngine;
using Chars.Tools;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource AudioSource;
    public AudioClip[] audioClipArray;
    private Dictionary<string, AudioClip> _dictionaryOfAudios;

    public void Start()
    {
        _dictionaryOfAudios = new Dictionary<string, AudioClip>();

        foreach (var item in audioClipArray)
        {
            _dictionaryOfAudios.Add(item.name, item);
        }
    }

    public AudioClip GetAudioClip(string key)
    {
        _dictionaryOfAudios.TryGetValue(key, out AudioClip currentAudio);
        return currentAudio;
    }

    public void PlayAudio(AudioClip currentAudio)
    {
        if (AudioSource == null) return;

        AudioSource.clip = currentAudio;
        AudioSource?.Play();
    }

   
    public void PlayAudioDelayed(AudioClip currentAudio, float delayed)
    {
        if (AudioSource == null) return;

        AudioSource.clip = currentAudio;
        AudioSource?.PlayDelayed(delayed);
    }

    public void PlayAudioOneShot(AudioClip currentAudio)
    {
        if (AudioSource == null) return;

        AudioSource?.PlayOneShot(currentAudio);
    }

    public void PlayAudioOneShot(AudioClip currentAudio, float volume)
    {
        if (AudioSource == null) return;

        AudioSource?.PlayOneShot(currentAudio, volume);
    }

    public void SetVolume(float value)
    {
        if (AudioSource == null) return;
        AudioSource.volume = value;
    }

}
