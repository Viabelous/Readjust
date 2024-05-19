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
    [SerializeField] private Text btnText;
    private Color currentColor;

    void Start()
    {
        currentColor = ImageComponent.color;
        // currState = NavigationState.Active;

    }
    void Update()
    {
        Left = WindowsController.FocusedButton;
        // hasUnlocked = GameManager.CheckUnlockedSkill(Left.GetComponent<SkillsSelection>().GetSkill().Name);

        switch (type)
        {
            case BtnType.Select:
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
        SkillsSelection skillSelected = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

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
                skillSelected.Upgrade();
                break;
            case BtnType.Locked:
                if (
                    GameManager.player.aerus < skillSelected.GetSkill().AerusCost &&
                    GameManager.player.exp < skillSelected.GetSkill().ExpCost
                )
                {
                    print("Uangnya kurang mba");
                }
                else
                {
                    GameManager.player.Pay(CostType.Exp, skillSelected.GetSkill().Cost);
                    GameManager.unlockedSkills.Add(skillSelected.GetSkill());

                    skillSelected.ChangeCurrentState(NavigationState.Hover);
                    WindowsController.HoveredButton = Left;
                    currState = NavigationState.Active;
                }
                break;
        }

        // currState = NavigationState.Selected;
        // // jika sebelumnya sudah diselect
        // if (IsSelected())
        // {
        //     GameManager.selectedSkills.Remove(prefab);
        // }
        // // jika belum pernah diselect
        // else
        // {
        //     // kalau slot belum penuh
        //     if (GameManager.selectedSkills.Count < 7)
        //     {
        //         GameManager.selectedSkills.Add(prefab);
        //         currState = NavigationState.Selected;
        //     }
        // }
    }
    public override void ExclusiveKey()
    {

    }
}