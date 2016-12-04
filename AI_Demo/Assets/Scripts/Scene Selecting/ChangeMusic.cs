using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{

    public AudioClip mainMenuMusic0;
    public AudioClip breadthMusic1;
    public AudioClip greedyMusic2;
    public AudioClip uniformMusic3;
    public AudioClip aStarMusic4;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnLevelWasLoaded(int level)
    {
        source.Stop();
        switch (level)
        {
            case 0:
                {
                    source.clip = mainMenuMusic0;
                    source.Play();
                }
                break;
            case 1:
                {
                    source.clip = breadthMusic1;
                    source.Play();
                }
                break;
            case 2:
                {
                    source.clip = greedyMusic2;
                    source.Play();
                }
                break;
            case 3:
                {
                    source.clip = uniformMusic3;
                    source.Play();
                }
                break;
            case 4:
                {
                    source.clip = aStarMusic4;
                    source.Play();
                }
                break;
            default:
                break;
        }
    }
}
