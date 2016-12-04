using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    bool beenCreated = false;
    //void Awake()
    //{
    //    if (!GameObject.Find("BackgroundMusic"))
    //        DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        if (!GameObject.Find("BackgroundMusic"))
            DontDestroyOnLoad(gameObject);
    }
}
