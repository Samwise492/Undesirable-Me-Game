using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioSource door, wayIn, stairs;
    public AudioSource stamp;

    void Awake()
    {
        if (GameObject.FindObjectOfType<SoundHandler>() == null)
            DontDestroyOnLoad(this);
    }
}
