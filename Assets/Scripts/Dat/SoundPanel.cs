using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    public Slider brg_slider;
    public Slider vfx_slider;
    public SoundEffect sound;
    public Button save;

    private void Start()
    {
        // Load trạng thái từ PlayerPrefs
        brg_slider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        vfx_slider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        StartCoroutine(FindPlayerSound());
        save.onClick.AddListener(OnButtonSave);
    }

    IEnumerator FindPlayerSound()
    {
        while (sound == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                sound = player.GetComponent<SoundEffect>();
            }

            if (sound == null)
            {
                Debug.Log("Player or SoundEffect not found. Retrying...");
                yield return null; // Chờ frame tiếp theo
            }
        }

        // Sau khi tìm thấy sound
        brg_slider.onValueChanged.AddListener(OnMusicVolumeChanged);
        vfx_slider.onValueChanged.AddListener(OnVFXVolumeChanged);

        // Cập nhật trạng thái ban đầu
        sound.AdjustMusicVolume(brg_slider.value);
        sound.AdjustVFXVolume(vfx_slider.value);

        Debug.Log("SoundEffect initialized successfully.");
    }

    void OnButtonSave()
    {
        Time.timeScale = 1;
        if (save != null)
        {
            if (sound == null)
            {
                sound = FindObjectOfType<SoundEffect>();
            }
            gameObject.SetActive(false);
            sound.PlaySound("Button");
        }
    }

    void OnToggleMusicChaged(bool b)
    {
        PlayerPrefs.SetInt("MusicOn", b ? 1 : 0);
        PlayerPrefs.Save();
        sound.ToggleMusic(b);
    }

    void OnToggleVFXChaged(bool b)
    {
        PlayerPrefs.SetInt("SFXOn", b ? 1 : 0);
        PlayerPrefs.Save();
        sound.ToggleVFX(b);
    }

    void OnMusicVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        sound.AdjustMusicVolume(volume);
    }

    void OnVFXVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
        sound.AdjustVFXVolume(volume);
    }
}
