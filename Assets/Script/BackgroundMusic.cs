using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource music;
    private GameManager gameManager;
    private bool isMusicStopped = false;
    public float fadeDuration = 5f;
    public float forceFadeOutDuration = 1f;
    public float fadeInVolume = 0.3f;
    public float fadeOutVolume = 0f;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>(); //Get the game manager to detect when stage is cleared
        music.volume = 0f;
        StartCoroutine(Fade(true, music, fadeDuration, fadeInVolume));
        StartCoroutine(Fade(false, music, fadeDuration, fadeOutVolume));
    }

    private void Update()
    {
        if (gameManager.checkWinCondition()) //Only go into this block when win condition is true
        {
            if (!isMusicStopped) //Force fade out if music is not stopped
            {
                StartCoroutine(ForceFadeOut(music, 0f));
            }
        }
        else if (!music.isPlaying && !isMusicStopped)
        {
            music.Play();
            StartCoroutine(Fade(true, music, fadeDuration, fadeInVolume));
            StartCoroutine(Fade(false, music, fadeDuration, fadeOutVolume));
        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource music, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthOfSource = (double)music.clip.samples / music.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }

        float time = 0f;
        float startVol = music.volume;
        while (time < duration)
        {
            //string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
            time += Time.deltaTime;
            music.volume = Mathf.Lerp(startVol, targetVolume, time/duration);
            yield return null;
        }
        yield break;
    }

    public IEnumerator ForceFadeOut(AudioSource music, float targetVolume)
    {
        // If music is already stopped or null, just exit the coroutine.
        if (music == null || !music.isPlaying)
        {
            yield break;
        }

        // Stop all other audio fade coroutines to prevent them from interfering.
        StopAllCoroutines();

        // Immediate reduction of volume to start the forced fade out.
        float fadeOutSpeed = music.volume / forceFadeOutDuration; // Calculate speed based on the original duration.
        while (music.volume > targetVolume)
        {
            // Reduce the volume based on fadeOutSpeed and deltaTime.
            music.volume -= fadeOutSpeed * Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to the exact target volume.
        music.volume = targetVolume;

        // If the target volume is 0 or less, stop the music.
        if (music.volume <= 0.01)
        {
            music.volume = 0;
            music.Stop();
        }
    }
}
