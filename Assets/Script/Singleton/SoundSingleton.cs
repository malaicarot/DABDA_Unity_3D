using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSingleton : Singleton<SoundSingleton>
{
    AudioSource backgroundMusic_1;
    AudioSource backgroundMusic_2;
    AudioSource backgroundMusic_3;


    AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic_1 = GetComponents<AudioSource>()[0];
        backgroundMusic_2 = GetComponents<AudioSource>()[1];
        backgroundMusic_3 = GetComponents<AudioSource>()[2];
    }

    void PlayAudio(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }

    void StopAudio(AudioSource audioSource)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Stop();
        }
    }

    public void BackgroundMusic()
    {
        StopAudio(backgroundMusic);
        int map = SceneManager.GetActiveScene().buildIndex;
        switch (map)
        {
            case 1:
                backgroundMusic = backgroundMusic_1;
                break;
            case 2:
                backgroundMusic = backgroundMusic_2;
                break;
            case 3:
                backgroundMusic = backgroundMusic_3;
                break;
        }
        PlayAudio(backgroundMusic);
    }
}
