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
    private Dictionary<string, AudioClip> soundDictionary;  // Từ điển để truy xuất âm thanh
    public AudioSource audioSource; // 
    void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        // Tạo dictionary từ danh sách âm thanh
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound.clip;
        }
    }

    // Hàm phát âm thanh dựa trên tên âm thanh
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
