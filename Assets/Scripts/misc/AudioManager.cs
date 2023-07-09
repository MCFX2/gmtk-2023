using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AudioEntry
{
    public AudioClip Clip;
    public string Name;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioEntry> _audioEntries = new();

    private static AudioManager _instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _instance = this;
    }

    public static void Play(string name)
    {
        _instance._audioSource.PlayOneShot(_instance._audioEntries.Find(c =>
            string.Compare(c.Name, name,
                StringComparison.InvariantCultureIgnoreCase) == 0).Clip);
    }
}
