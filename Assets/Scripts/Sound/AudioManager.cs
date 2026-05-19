using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip buttonClick;
    public AudioClip playerFootstepSound;
    public AudioClip enemyFootstepSound;
    public AudioClip dashSound;
    public AudioClip enemyTeleportSound;
    public AudioClip playerBulletHit;
    public AudioClip enemyBulletHit;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
