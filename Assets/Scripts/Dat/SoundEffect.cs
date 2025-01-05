using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioClip> soundDictionary;
    
    // AudioSource cho nhạc nền và hiệu ứng âm thanh
    public AudioSource musicAudioSource;
    public AudioSource effectsAudioSource;
    
    [Range(0f, 1f)]
    public float soundEffectVolume = 0.0f;
    [Range(0f, 1f)]
    public float musicVolume = 0.0f;

    void Awake()
    {
        if (musicAudioSource == null)
        {
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (effectsAudioSource == null)
        {
            effectsAudioSource = gameObject.AddComponent<AudioSource>();
        }

        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound.clip;
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            effectsAudioSource.PlayOneShot(soundDictionary[soundName], soundEffectVolume);
        }
        else
        {
            Debug.LogWarning("Âm thanh: " + soundName + " không tìm thấy!");
        }
    }

    public void PlayMusic(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            musicAudioSource.clip = soundDictionary[soundName];
            musicAudioSource.loop = true;
            musicAudioSource.volume = musicVolume;
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Âm thanh: " + soundName + " không tìm thấy!");
        }
    }

    public void ToggleMusic(bool isOn)
    {
        musicAudioSource.mute = !isOn; // Tắt hoặc bật nhạc nền
    }

    public void ToggleVFX(bool isOn)
    {
        effectsAudioSource.mute = !isOn; // Tắt hoặc bật âm thanh hiệu ứng
    }

    public void AdjustMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicAudioSource.volume = musicVolume;
    }

    public void AdjustVFXVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
        effectsAudioSource.volume = soundEffectVolume;
    }
}
