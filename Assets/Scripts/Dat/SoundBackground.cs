using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundBackground : MonoBehaviour
{
    [SerializeField] SoundEffect sound;

    private void Start()
    {
            sound = FindObjectOfType<SoundEffect>();
            sound.PlayMusic("Background");
    }
}
