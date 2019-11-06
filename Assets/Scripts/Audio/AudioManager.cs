using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    // Singleton Variable
    public static AudioManager instance;

    public Sound[] sounds;

    public void Awake() {
        // Ensures only one instance of the AudioManager exists at a time
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);

        // Load in every sound
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    // Setting the ambient sounds depending on the current room the user is in
    public void Start() {
        string curRoom = SceneManager.GetActiveScene().name;

        switch (curRoom) {
            case "EntryDoor":
                PlaySound("EntryAmbience");
                break;
            case "Room1":
                PlaySound("Room1Ambience");
                break;
            case "Room2":
                PlaySound("Room2Ambience");
                break;
            case "WalkWay":
                PlaySound("WalkwayFootsteps1");
                PlaySound("WalkwayFootssteps2");
                break;
        }
    }

    // Attemps to find a sound the name to play it
    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Failsafe in case a sound doesn't exist
        if (s == null) {
            Debug.Log("Sound doesn't exist!");
            return;
        }
        s.source.Play();
    }

    // Attempts to find a sound given the name to stop if from playing
    public void StopSound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Failsafe in case a sound doesn't exist
        if (s == null) {
            Debug.Log("Sound doesn't exists");
            return;
        }
        // Only stop the sound if it is actually playing
        if (s.source.isPlaying) {
            s.source.Stop();
        }
    }

    // Stop all sounds that are currently playing
    public void StopAllSounds() {
        foreach(Sound s in sounds) {
            StopSound(s.name);
        }
    }
}
