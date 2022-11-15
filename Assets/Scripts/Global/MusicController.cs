using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> musicTracks = new List<AudioClip>();
    public int trackNumber = -1;
    private AudioSource audioSource;

    public float fadeInTime = 1f;
    private float fadeTimer = 0f;

    public float maxVolume = 0.75f;

    public bool skipSongDebug = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (skipSongDebug)
        {
            NextSong();
            fadeTimer = 0f;
            skipSongDebug = false;
        }
        if (!audioSource.isPlaying)
        {
            NextSong();
            fadeTimer = 0f;
        }
        if (fadeTimer <= fadeInTime)
        {
            audioSource.volume = (fadeTimer / fadeInTime) * maxVolume;
            fadeTimer += Time.deltaTime;
        }
        else
            audioSource.volume = maxVolume;
    }

    /// <summary>
    /// Moves to the next song
    /// </summary>
    private void NextSong()
    {
        trackNumber++;
        if (trackNumber >= musicTracks.Count)
        {
            trackNumber = 0;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(musicTracks[trackNumber]);
    }
}
