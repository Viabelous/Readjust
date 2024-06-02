using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
    }
}
