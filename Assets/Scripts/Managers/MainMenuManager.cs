using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    [SerializeField] private Player playerBasic;

    [SerializeField] private Image newBtn, loadBtn, exitBtn;
    private Image currentBtn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentBtn = newBtn;
        newBtn.color = SelectedColor(currentBtn);
        loadBtn.color = UnSelectedColor(loadBtn);
        exitBtn.color = UnSelectedColor(exitBtn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentBtn.name == newBtn.name)
            {
                ToggleBtn(loadBtn);
            }
            else if (currentBtn.name == loadBtn.name)
            {
                ToggleBtn(exitBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentBtn.name == loadBtn.name)
            {
                ToggleBtn(newBtn);
            }
            else if (currentBtn.name == exitBtn.name)
            {
                ToggleBtn(loadBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentBtn.name == newBtn.name)
            {
                CreateNewData();
                SceneManager.LoadScene("DeveloperZone");
            }
            else if (currentBtn.name == loadBtn.name)
            {
                LoadGameData();
                SceneManager.LoadScene("DeveloperZone");

            }
            else if (currentBtn.name == exitBtn.name)
            {
                Application.Quit();
            }
        }
    }

    private void ToggleBtn(Image otherBtn)
    {
        Image prevBtn = currentBtn;
        currentBtn = otherBtn;
        otherBtn = prevBtn;

        currentBtn.color = SelectedColor(currentBtn);
        otherBtn.color = UnSelectedColor(otherBtn);
    }

    private Color SelectedColor(Image btn)
    {
        Color color = btn.color;
        color.r = 1f;
        color.g = 1f;
        color.b = 1f;
        color.a = 1f;
        return color;
    }

    private Color UnSelectedColor(Image btn)
    {
        Color color = btn.color;
        color.r = 0.8f;
        color.g = 0.8f;
        color.b = 0.8f;
        color.a = 0.8f;
        return color;
    }

    private void CreateNewData()
    {
        GameManager.player = playerBasic.Clone();
        DataManager.SavePlayer(GameManager.player);
    }

    private void LoadGameData()
    {
        GameManager.player = playerBasic.Clone();
        GameManager.player.LoadData(DataManager.LoadPlayer());
    }


}