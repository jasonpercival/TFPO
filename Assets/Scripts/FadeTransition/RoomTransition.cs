using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransition : MonoBehaviour {
    public Animator animator;
    private int levelIndex = 0;

    public void FadeToLevel(int levelIndex) {
        this.levelIndex = levelIndex;
        animator.SetTrigger("fadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelIndex);
    }
}
