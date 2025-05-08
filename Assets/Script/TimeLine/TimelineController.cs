using UnityEngine.Playables;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector[] playableDirector;
    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponents<PlayableDirector>();
            
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayTimeline(int index)
    {
        if (playableDirector != null)
        {
            playableDirector[index].Play();
        }
    }

    public void StopTimeline(int index)
    {
        if (playableDirector != null)
        {
            playableDirector[index].Stop();
        }
    }
}
