using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect { Woodblock, Correct, Incorrect, Finished, Attack, EatFood}
public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField]
    private AudioClip[] audioClips = default;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(SoundEffect soundEffect)
    {
        if (soundEffect == SoundEffect.Attack || soundEffect == SoundEffect.Woodblock || soundEffect == SoundEffect.EatFood)
        {
            audioSource.volume = .15f;
        }
        else
        {
            audioSource.volume = 1f;
        }

        audioSource.clip = audioClips[(int)soundEffect];
        audioSource.Play();
    }
}