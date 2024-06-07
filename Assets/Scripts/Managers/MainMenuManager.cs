using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public LevelChanger levelChanger;

    [SerializeField] private Image newBtn, loadBtn, exitBtn, creditBtn, credit, okBtn;
    private Image currentBtn;

    // sound
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip buttonSelectedAudio, buttonChangeAudio, transitionAudio;

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
        creditBtn.color = UnSelectedColor(creditBtn);
        credit.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentBtn.name == newBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(loadBtn);
            }
            else if (currentBtn.name == loadBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(exitBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentBtn.name == loadBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(newBtn);
            }
            else if (currentBtn.name == exitBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(loadBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentBtn.name == loadBtn.name || currentBtn.name == newBtn.name || currentBtn.name == exitBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(creditBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentBtn.name == creditBtn.name)
            {
                PlaySound(buttonChangeAudio);
                ToggleBtn(newBtn);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentBtn.name == newBtn.name)
            {
                PlaySound(buttonSelectedAudio);
                LoadSaveDataManager.instance.CreateNewData();
                levelChanger.Transition("DeveloperZone");
            }
            else if (currentBtn.name == loadBtn.name)
            {
                PlaySound(buttonSelectedAudio);
                LoadSaveDataManager.instance.LoadGameData();
                levelChanger.Transition("DeveloperZone");
            }
            else if (currentBtn.name == exitBtn.name)
            {
                PlaySound(buttonSelectedAudio);
                Application.Quit();
            }
            else if (currentBtn.name == creditBtn.name)
            {
                PlaySound(buttonSelectedAudio);
                OpenCredit();
            }
            else if (currentBtn.name == okBtn.name)
            {
                PlaySound(buttonSelectedAudio);
                CloseCredit();
            }
        }
    }

    void OpenCredit()
    {
        credit.gameObject.SetActive(true);
        ToggleBtn(okBtn);
    }

    void CloseCredit()
    {
        credit.gameObject.SetActive(false);
        ToggleBtn(creditBtn);
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

    void PlaySound(AudioClip audio)
    {
        audioSrc.clip = audio;
        audioSrc.Play();
    }


}