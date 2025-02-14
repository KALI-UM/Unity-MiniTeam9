using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SoundManager : Singleton<SoundManager>
{
    private AudioClipPackData currentAudioClipPack;

    private AudioSource audioBgm;
    private AudioSource audioSfx;
    

    private Dictionary<string, AudioClip> audioDictionary = new();

    private void Awake()
    {
        audioBgm = gameObject.AddComponent<AudioSource>();
        audioBgm.loop = true;
        audioSfx = gameObject.AddComponent<AudioSource>();
    }

    public void SetAudioClipPack(AudioClipPackData currentAudioClips)
    {
        currentAudioClipPack = currentAudioClips;

        audioDictionary.Clear();
        foreach (AudioClip clip in currentAudioClipPack.bgms)
        {
            audioDictionary.Add(clip.name, clip);
        }
        foreach (AudioClip clip in currentAudioClipPack.sfxs)
        {
            audioDictionary.Add(clip.name, clip);
        }
    }

    public void PlayBGM(string key)
    { 
        audioBgm.clip = audioDictionary[key];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySFX(string key)
    {
        audioSfx.PlayOneShot(audioDictionary[key]);
    }

}