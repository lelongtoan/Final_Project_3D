using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionWithEnenemy : MonoBehaviour
{
    public SoundEffect soundEffect;
    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            soundEffect.PlaySound("Attack");
        }
    }
}
