using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer MusicaudioMixer;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void MusicVolume(float volume)
    {
        MusicaudioMixer.SetFloat("music", volume);
    }
}
