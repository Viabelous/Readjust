using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenStage()
    {
        SceneManager.LoadScene("Stage1");

    }
}
