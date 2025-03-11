using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    void Start()
    {
        SoundSingleton._instance.BackgroundMusic();

    }
}
