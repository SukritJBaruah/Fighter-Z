using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.kiblast,
            Resources.Load<AudioClip>("kiblast"));
        audioClips.Add(AudioClipName.kiplosion,
            Resources.Load<AudioClip>("kiplosion"));
        audioClips.Add(AudioClipName.finalflash_charge,
            Resources.Load<AudioClip>("finalflash_charge"));
        audioClips.Add(AudioClipName.finalflash,
             Resources.Load<AudioClip>("finalflash"));
        audioClips.Add(AudioClipName.explosion,
             Resources.Load<AudioClip>("explosion"));
        audioClips.Add(AudioClipName.bigbang_fire,
              Resources.Load<AudioClip>("bigbang_fire"));

    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
