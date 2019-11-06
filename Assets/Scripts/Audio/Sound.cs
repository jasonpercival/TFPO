using UnityEngine;

[System.Serializable]
public class Sound {
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;      // The audio file itself
    public string name;         // The name given to the sound in the inspector
    public bool loop;           // If true, the sound will loop until it is stopped
}
