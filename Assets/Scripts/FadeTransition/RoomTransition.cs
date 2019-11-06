using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour {
    public static RoomTransition instance;
    public Animator animator;
    private string levelName = "";

    public void Awake() {
        // Ensures only one instance of the AudioManager exists at a time
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void FadeToLevel(string levelName) {
        this.levelName = levelName;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete() {
        AudioManager.instance.StopAllSounds();
        SceneManager.LoadScene(levelName);
    }
}
