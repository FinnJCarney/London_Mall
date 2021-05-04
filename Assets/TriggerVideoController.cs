using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerVideoController : MonoBehaviour
{
    private MeshRenderer mr;
    [SerializeField] private string trigTag;

    [Header("Video Components")]
    [SerializeField] GameObject videoHolder;
    [SerializeField] VideoPlayer videoPlayer;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();

        mr.enabled = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(trigTag))
        {
            if (videoHolder.active == false)
            {
                StartFilm();
            }
        }
    }

    void StartFilm()
    {
        videoHolder.active = true;
        videoPlayer.Play();
    }
}
