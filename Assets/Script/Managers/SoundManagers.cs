using UnityEngine;
using UnityEngine.UI;

public class SoundManagers : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // float savedVolume = PlayerPrefs.GetFloat("AudioVolume", 0.75f);
        // volumeSlider.value = savedVolume;
        // SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Slider" + volumeSlider.value);
        Debug.Log(volume);
        SoundSingleton._instance.SetVolume(volume);
    }


}
