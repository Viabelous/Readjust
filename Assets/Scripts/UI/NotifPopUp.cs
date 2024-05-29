using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopUpBtnType
{
    None,
    OK,
    Cancel
}
public enum PopUpType
{
    OKCancel,
    OK,
}

public class NotifPopUp : MonoBehaviour
{


    [HideInInspector] public string id;
    [HideInInspector] public string info;
    [SerializeField] private PopUpType type;
    [SerializeField] private Text infoText;
    [SerializeField] List<Image> buttons;
    [SerializeField] Image currentBtn;
    private Color initColor, unselectColor;
    private PopUpBtnType btnClicked;

    void Start()
    {
        currentBtn = buttons[0];
        initColor = currentBtn.color;

        if (type == PopUpType.OKCancel)
        {
            unselectColor = buttons[1].color;
        }

        // currentBtn.color = SelectedColor(currentBtn);

        infoText.text = info;
        btnClicked = PopUpBtnType.None;
    }

    void Update()
    {
        switch (type)
        {
            case PopUpType.OKCancel:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ChangeCurrentBtn(buttons[1]);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ChangeCurrentBtn(buttons[0]);
                }
                break;
        }

    }

    private Color SelectedColor(Image btn)
    {
        Color color = initColor;
        return color;
    }

    private Color UnSelectedColor(Image btn)
    {
        Color color = unselectColor;
        return color;
    }

    private void ChangeCurrentBtn(Image btn)
    {
        currentBtn.color = UnSelectedColor(currentBtn);
        currentBtn = btn;
        currentBtn.color = SelectedColor(btn);
    }

    public Image GetCurrentBtn()
    {
        return currentBtn;
    }

    public void SetClickedBtn(PopUpBtnType btnType)
    {
        btnClicked = btnType;
    }

    public PopUpBtnType GetClickedBtn()
    {
        return btnClicked;
    }
}