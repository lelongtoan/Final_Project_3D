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
    public AudioSource audioSource; 
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("AudioSource không tìm thấy. Tạo mới AudioSource trên GameObject này.");
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
            audioSource.PlayOneShot(soundDictionary[soundName]);
        }
        else
        {
            Debug.LogWarning("Âm thanh: " + soundName + " không tìm thấy!");
        }
    }
}
