using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public AudioSource mixer;
    public float volume = 1f;
    public Slider volumeslider;

    void Start()
    {
        volumeslider.value = PlayerPrefs.GetFloat("Volume");
        mixer.volume = PlayerPrefs.GetFloat("Volume");
    }

    void Update()
    {
        mixer.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void SetVolume(float volumeval)
    {
        PlayerPrefs.SetFloat("Volume", volumeval);
        volume = volumeval;
    }

}
