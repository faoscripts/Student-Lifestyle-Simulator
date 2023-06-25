using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    public bool autoPlay;
    [Header("3d options")]
    public bool spatialBlend;
    public float minDistance;
    public float maxDistance;
    [HideInInspector]
    public AudioSource source;
    public GameObject sourceGO;
    public string tagObject;
}