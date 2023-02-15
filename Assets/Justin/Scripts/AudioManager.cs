using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        [HideInInspector]public AudioSource source;
        public string clipName;
        [Space]
        public AudioClip clip;
        [Space]
        public AudioMixerGroup group;
        [Space]
        [Range(0, 1)] public float volume;
        [Range(0, 3)] public float pitch;
        public bool loop;
    }

    public Sound[] sounds;

    public static AudioManager instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);

        if(s == null)
        {
            Debug.LogError("Sound: " + name + " not found!, the sound your trying to play might not exist or you have the name wrong.");
            return;
        }

        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.clipName == sound);

        if(s == null)
        {
            Debug.LogError("Sound: " + sound + " not found!, the sound your trying to stop might not exist or you have the name wrong.");
            return;
        }

        s.source.Stop();
    }
}
