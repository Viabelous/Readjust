using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    StageState type;
    [SerializeField] private Text status, score, time, aerus, extraAerus, exp, extraExp;
    [SerializeField] private Image currentBtn, otherBtn;
    [SerializeField] private Sprite loseBanner, winBanner;
    // private Color selectedColor, unselectedColor;
    // private bool instantiated = false;

    void Start()
    {
        currentBtn.color = SelectedColor(currentBtn);
        otherBtn.color = UnSelectedColor(otherBtn);
    }

    void Update()
    {
        if (
            currentBtn.name == "replay_btn" && Input.GetKeyDown(KeyCode.RightArrow) ||
            currentBtn.name == "menu_btn" && Input.GetKeyDown(KeyCode.LeftArrow)
        )
        {
            ToggleBtn();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentBtn.name == "menu_btn")
        {
            StageManager.instance.ResumeTime();
            SceneManager.LoadScene("DeveloperZone");
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentBtn.name == "replay_btn")
        {

        }

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
        color.a = 0.5f;
        return color;
    }

    private void ToggleBtn()
    {
        Image prevBtn = currentBtn;
        currentBtn = otherBtn;
        otherBtn = prevBtn;

        currentBtn.color = SelectedColor(currentBtn);
        otherBtn.color = UnSelectedColor(otherBtn);
    }

    public void SetType(StageState type)
    {
        this.type = type;
        switch (type)
        {
            case StageState.Win:
                GetComponent<Image>().sprite = winBanner;
                status.text = "WIN";
                break;
            case StageState.Lose:
                GetComponent<Image>().sprite = loseBanner;
                status.text = "LOSE";
                break;
        }
    }
    // public void SetStatus(string status)
    // {
    //     this.status.text = status;
    // }

    public void SetScore(float score)
    {
        this.score.text = score.ToString();
    }
    public void SetEndTime(string time)
    {
        this.time.text = time;
    }

    public void SetEndAerus(float aerus, float extra)
    {
        this.aerus.text = aerus.ToString();
        this.extraAerus.text = extra != 0 ? "+" + extra.ToString() : "";

    }

    public void SetEndExp(float exp, float extra)
    {
        this.exp.text = exp.ToString();
        this.extraExp.text = extra != 0 ? "+" + extra.ToString() : "";

    }

}