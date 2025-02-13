using UnityEngine;

public class PlaySound : MonoBehaviour
{
    void Start()
    {
        SoundSingleton._instance.BackgroundMusic();
    }
}
