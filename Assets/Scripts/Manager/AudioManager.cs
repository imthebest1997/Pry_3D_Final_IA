using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] musicSource;
    public AudioClip[] sfxSource;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource sfxAudioSource;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource.clip = musicSource[0];
        audioSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSource.clip = clip;
        sfxAudioSource.PlayOneShot(clip);
    }
}
 