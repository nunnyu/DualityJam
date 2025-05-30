using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageAudio : MonoBehaviour
{
    public static ManageAudio Instance { get; private set; }
    public Sound[] sounds;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ensures there's only one 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.Play();
    }

    public void Stop(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.isPlaying)
        {
            return true;
        }

        return false;
    }
}