using UnityEngine.Playables;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }

    public void PlayTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
    }

    public void StopTimeline(){
        if(playableDirector != null){
            playableDirector.Stop();
        }
    }
}
