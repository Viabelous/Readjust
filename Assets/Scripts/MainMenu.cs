using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;

    public int levelToLoad;

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void PlayGame()
    {
        if(levelToLoad == 0) {
            SceneManager.LoadScene("DeveloperZone");
        
        } else {
            SceneManager.LoadScene("Stage"+levelToLoad);
        }
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
