using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceBGM;
    [SerializeField] private AudioClip audioJump;
    [SerializeField] private AudioClip audioPickup;
    [SerializeField] private AudioClip audioHit;
    [SerializeField] private AudioClip audioLvComplete;
    [SerializeField] private AudioClip[] audioMusic;

    public enum AudioType { Jump, PickUp, Hit, LevelComplete };
    public void PlayAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Jump:
                audioSource.clip = audioJump;
                break;
            case AudioType.PickUp:
                audioSource.clip = audioPickup;
                break;
            case AudioType.Hit:
                audioSource.clip = audioHit;
                break;
            case AudioType.LevelComplete:
                audioSource.clip = audioLvComplete;
                break;
        }
        audioSource.Play();
    }

    public void PlayBGM(int index)
    {
        audioSourceBGM.clip = audioMusic[index];
        audioSourceBGM.Play();
    }
}
