using UnityEngine.Playables;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    // public static TimelineController singletonTimeLine;
    public PlayableDirector playableDirector;
    // Start is called before the first frame update

    // void Awake() {
    //     if(singletonTimeLine == null){
    //         singletonTimeLine = this;
    //         DontDestroyOnLoad(gameObject);
    //     }else{
    //         Destroy(gameObject);
    //     }
        
    // }
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
