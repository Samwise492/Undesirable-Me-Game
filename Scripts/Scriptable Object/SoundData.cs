using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundData : ScriptableObject
{
    public List<Sound> sounds;
}

[Serializable]
public class Sound
{
    public string _name;
    public AudioClip audioClip;
}