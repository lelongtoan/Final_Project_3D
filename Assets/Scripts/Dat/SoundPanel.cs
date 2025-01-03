using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanel : MonoBehaviour
{
    public Slider brg_slider;
    public Slider vfx_slider;
    public Toggle brg_toggle;
    public Toggle vfx_toggle;
    private SoundEffect sound;

    private void Start()
    {
        // Load trạng thái từ PlayerPrefs
        brg_toggle.isOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        vfx_toggle.isOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        brg_slider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        vfx_slider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        StartCoroutine(FindPlayerSound());
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
            yield return null;
        }

        if (sound != null)
        {
            brg_toggle.onValueChanged.AddListener(OnToggleMusicChaged);
            vfx_toggle.onValueChanged.AddListener(OnToggleVFXChaged);
            brg_slider.onValueChanged.AddListener(OnMusicVolumeChanged);
            vfx_slider.onValueChanged.AddListener(OnVFXVolumeChanged);

            // Cập nhật trạng thái ban 
            sound.ToggleMusic(brg_toggle.isOn);
            sound.ToggleVFX(vfx_toggle.isOn);
            sound.AdjustMusicVolume(brg_slider.value);
            sound.AdjustVFXVolume(vfx_slider.value);

            Debug.Log("SoundEffect initialized.");
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
