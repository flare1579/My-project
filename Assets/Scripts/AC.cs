using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AC : MonoBehaviour
{
    public AudioMixer Am;

    public GameObject coinSFX;
    public GameObject   HeartSFX;
    public GameObject dmgSFX;
    public GameObject CSFX;
    public void PlaySFX(string Tag)
    {
        switch (Tag)
        {
            case "coin":
            coinSFX.GetComponent<AudioSource>().Play();
               return;
            case "HPadd":
                HeartSFX.GetComponent<AudioSource>().Play();
                return;
            case "HPlose":
                dmgSFX.GetComponent<AudioSource>().Play();
                return;
            case "CSFX":
                CSFX.GetComponent<AudioSource>().Play();
                return;

        }
   }
    public void UpdateSFXGroup(float NewV)
    {
        Am.SetFloat("SFXV", Mathf.Log10(NewV) * 20);
    }
    public void UpdateMusicGroup(float NewV)
    {
        Am.SetFloat("MusicV", Mathf.Log10(NewV) * 20);
    }
}
