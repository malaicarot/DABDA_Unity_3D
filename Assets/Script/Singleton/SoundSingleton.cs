using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSingleton : Singleton<SoundSingleton>
{
    AudioSource backgroundMusic_1;
    AudioSource backgroundMusic_2;
    AudioSource backgroundMusic_3;
    AudioSource backgroundMusic_4;
    AudioSource backgroundMusic_5;

    AudioSource endBackgroundMusic;


    AudioSource getItem;
    AudioSource equipItem;


    AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic_1 = GetComponents<AudioSource>()[0];
        backgroundMusic_2 = GetComponents<AudioSource>()[1];
        backgroundMusic_3 = GetComponents<AudioSource>()[2];
        backgroundMusic_4 = GetComponents<AudioSource>()[3];
        backgroundMusic_5 = GetComponents<AudioSource>()[4];
        endBackgroundMusic = GetComponents<AudioSource>()[7];

        getItem = GetComponents<AudioSource>()[5];
        equipItem = GetComponents<AudioSource>()[6];
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
        if (backgroundMusic != null)
        {
            StopAudio(backgroundMusic);
        }
        if(endBackgroundMusic != null){
            StopAudio(endBackgroundMusic);
        }

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
            case 4:
                backgroundMusic = backgroundMusic_4;
                break;
            case 5:
                backgroundMusic = backgroundMusic_5;
                break;

        }
        PlayAudio(backgroundMusic);
    }

    public void EndMusic(){
        if (backgroundMusic != null)
        {
            StopAudio(backgroundMusic);
        }
        PlayAudio(endBackgroundMusic);
    }

    public void GetItem()
    {
        StopAudio(getItem);
        PlayAudio(getItem);
    }

    public void EquipItem()
    {
        StopAudio(equipItem);
        PlayAudio(equipItem);
    }
}
