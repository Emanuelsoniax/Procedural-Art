using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGenerator : MonoBehaviour
{
    public int index;
    private float frequency;
    private int sampleRate = 44100;
    [SerializeField]
    private float duration = 1;
    
    public void SetFrequency()
    {
        frequency = Frequencies.frequencies[index];
    }

    public void PlayAudio()
    {
        Debug.Log(this.name);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(CreateClip());
        StartCoroutine(FadeOut());
 
    }

    private IEnumerator FadeOut()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume* Time.deltaTime / duration;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public AudioClip CreateClip()
    {
        AudioClip clip = AudioClip.Create(this.name, (int)(duration * sampleRate), 1, sampleRate, false);

        var size = clip.frequency * (int)Mathf.Ceil(clip.length);
        float[] data = new float[size];

        for (int i = 0; i < size; ++i)
        {
            float lam = (float)i / (float)sampleRate * frequency % 1f;
            data[i] = lam > 0.5f ? 0.75f : -0.75f;
        }

        clip.SetData(data, 0);
        return clip;
    }
}

    static class Frequencies
{
    public static float[] frequencies =
    {
       62.5f,
       55,
       50,
       44,
       37.5f,
       35,
        
       125,
       110,
       100,
       88,
       75,
       70,

       250,
       220,
       200,
       175,
       150,
       140,

       500,
       440,
       400,
       350,
       300,
       280,

       1000,
       880,
       800,
       700,
       600,
       560,

       2000,
       1760,
       1600,
       1400,
       1200,
       1120,

       4000,
       3520,
       3200,
       2800,
       2400,
       2240
    };
}