using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DeveloperZone");
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
