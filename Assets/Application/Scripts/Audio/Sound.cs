using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;

    [Range(0, 1f)] public float Volume = 0.7f;
}