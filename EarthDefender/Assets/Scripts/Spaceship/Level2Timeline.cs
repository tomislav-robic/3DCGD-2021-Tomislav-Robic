using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class Level2Timeline : MonoBehaviour
{
    private void Start()
    {
        PlayableDirector director = GetComponent<PlayableDirector>();
        TimelineAsset timeline = (TimelineAsset)director.playableAsset;
        director.SetGenericBinding(timeline.GetOutputTrack(1), SpaceshipHealth.i.GetComponent<Animator>());
        director.SetGenericBinding(timeline.GetOutputTrack(2), SpaceshipHealth.i.GetComponent<AudioSource>());
    }

    public void EnableAnimator()
    {
        SpaceshipHealth.i.GetComponent<Animator>().enabled = true;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
