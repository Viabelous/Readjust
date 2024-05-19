using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsBtnSelection : Navigation
{
    enum BtnType
    {
        Select, Upgrade, Locked
    }

    [SerializeField] private BtnType type;
    [SerializeField] private NavigationState currState;
    [SerializeField] private Text btnText, costText;
    private Color currentColor;
    private SkillsSelection skillSelected;

    void Start()
    {
        currentColor = ImageComponent.color;
        // currState = NavigationState.Active;

    }
    void Update()
    {
        Left = WindowsController.FocusedButton;
        skillSelected = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

        // hasUnlocked = GameManager.CheckUnlockedSkill(Left.GetComponent<SkillsSelection>().GetSkill().Name);

        switch (type)
        {
            case BtnType.Select:

                if (Down != null && !Down.activeInHierarchy)
                {
                    Down = null;
                }

                if (Left.GetComponent<SkillsSelection>().HasSelected())
                {
                    btnText.text = "UNSELECT";
                }
                else
                {
                    btnText.text = "SELECT";
                }


                break;

            case BtnType.Upgrade:
                costText.text = skillSelected.GetSkill().ExpUpCost.ToString();
                break;
            case BtnType.Locked:
                costText.text = skillSelected.GetSkill().ExpUnlockCost.ToString();
                break;
        }

        switch (currState)
        {
            case NavigationState.Active:
                if (WindowsController.HoveredButton == gameObject)
                {
                    currState = NavigationState.Hover;
                }

                currentColor.r = 0.5f;
                currentColor.g = 0.5f;
                currentColor.b = 0.5f;
                ImageComponent.color = currentColor;
                break;

            case NavigationState.Hover:
                currentColor.r = 1f;
                currentColor.g = 1f;
                currentColor.b = 1f;
                ImageComponent.color = currentColor;
                break;
        }
    }

    public override void IsHovered(bool state)
    {
        if (state)
        {
            currState = NavigationState.Hover;
        }
        else
        {
            currState = NavigationState.Active;
        }
    }

    public override void Clicked()
    {
        switch (type)
        {
            case BtnType.Select:
                if (skillSelected.HasSelected())
                {
                    skillSelected.Unselected();
                }
                else
                {
                    skillSelected.Select();
                }
                break;

            case BtnType.Upgrade:
                if (GameManager.player.exp < skillSelected.GetSkill().ExpUpCost)
                {
                    print("Uangnya kurang mba");
                }
                else
                {
                    skillSelected.Upgrade();

                    if (skillSelected.GetSkill().Level == skillSelected.GetSkill().MaxLevel)
                    {
                        HoverBackToSkill(skillSelected);
                        gameObject.SetActive(false);
                    }

                }
                break;

            case BtnType.Locked:
                if (GameManager.player.exp < skillSelected.GetSkill().ExpUnlockCost)
                {
                    print("Uangnya kurang mba");
                }
                else
                {
                    GameManager.player.Pay(CostType.Exp, skillSelected.GetSkill().Cost);
                    GameManager.unlockedSkills.Add(skillSelected.GetSkill());

                    HoverBackToSkill(skillSelected);

                }
                break;
        }
    }

    public override void ExclusiveKey()
    {

    }

    private void HoverBackToSkill(SkillsSelection skillSelected)
    {
        skillSelected.ChangeCurrentState(NavigationState.Hover);
        WindowsController.HoveredButton = Left;
        currState = NavigationState.Active;
    }
}