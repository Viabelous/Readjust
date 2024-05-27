using System.Collections;
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
    private SkillsSelection focusedSkill;

    public SkillWindowsBtnSelection()
    {
    }

    void Start()
    {
        currentColor = ImageComponent.color;
        // currState = NavigationState.Active;

    }
    void Update()
    {
        Left = WindowsController.FocusedButton;
        focusedSkill = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

        // hasUnlocked = GameManager.CheckUnlockedSkill(Left.GetComponent<SkillsSelection>().GetSkill().Name);

        switch (type)
        {
            case BtnType.Select:

                if (Down != null && !Down.activeInHierarchy)
                {
                    Down = null;
                }

                else
                {
                    Down = GameObject.Find("upgrade_btn");
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
                costText.color = GameManager.player.exp < focusedSkill.GetSkill().ExpUpCost ? Color.red : Color.black;

                costText.text = focusedSkill.GetSkill().ExpUpCost.ToString();
                break;

            case BtnType.Locked:
                costText.color = GameManager.player.exp < focusedSkill.GetSkill().ExpUnlockCost ? Color.red : Color.black;
                costText.text = focusedSkill.GetSkill().ExpUnlockCost.ToString();
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

        if (ZoneManager.instance.CurrentState() == ZoneState.OnPopUp)
        {
            switch (WindowsController.popUp.id)
            {
                case "upgrade_failed":
                case "unlock_failed":
                    if (WindowsController.popUp.GetClickedBtn() == PopUpBtnType.OK)
                    {
                        Destroy(WindowsController.popUp.gameObject);
                        ZoneManager.instance.ChangeCurrentState(ZoneState.Idle);
                    }
                    break;
            }
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
        // StartCoroutine(ClickedAnimation());
        Skill skill = focusedSkill.GetSkill();

        switch (type)
        {
            // masukkan skill ke slot --------------------------------------------------------

            case BtnType.Select:
                if (focusedSkill.HasSelected())
                {
                    focusedSkill.Unselected();
                }
                else
                {
                    focusedSkill.Select();
                }
                break;

            // upgrade skill --------------------------------------------------------

            case BtnType.Upgrade:
                if (GameManager.player.exp < skill.ExpUpCost)
                {
                    WindowsController.CreatePopUp(
                        "upgrade_failed",
                        PopUpType.OK,
                        "Anda membutuhkan lebih banyak Exp Orb untuk meningkatkan skill ini."
                    );
                }

                else
                {
                    focusedSkill.Upgrade();
                    GameManager.player.Pay(CostType.Exp, skill.ExpUpCost);

                    if (skill.Level == skill.MaxLevel)
                    {
                        HoverBackToSkill(focusedSkill);
                        gameObject.SetActive(false);
                    }

                }
                break;

            // beli / buka skill baru --------------------------------------------------------
            case BtnType.Locked:
                if (GameManager.player.exp < skill.ExpUnlockCost)
                {
                    WindowsController.CreatePopUp(
                        "unlock_failed",
                        PopUpType.OK,
                        "Anda membutuhkan lebih banyak Exp Orb untuk membuka skill ini."
                    );
                }
                else
                {
                    GameManager.player.Pay(CostType.Exp, skill.ExpUnlockCost);
                    GameManager.unlockedSkills.Add(skill.Name, skill.Level);
                    IncreaseElementSkillProgress(skill);
                }
                break;
        }
    }

    // private IEnumerator ClickedAnimation()
    // {
    //     currState = NavigationState.Active;
    //     yield return new WaitForSeconds(0.5f);
    //     currState = NavigationState.Hover;
    // }

    public override void ExclusiveKey()
    {

    }

    private void HoverBackToSkill(SkillsSelection focusedSkill)
    {
        focusedSkill.ChangeCurrentState(NavigationState.Hover);
        WindowsController.HoveredButton = Left;
        WindowsController.FocusedButton = null;
        currState = NavigationState.Active;
    }

    public void SetCostColor(Color color)
    {
        costText.color = color;
    }

    private void IncreaseElementSkillProgress(Skill skill)
    {
        switch (skill.Element)
        {
            case Element.Fire:
                GameManager.player.IncreaseProgress(Player.Progress.FireSkill, 1);
                break;
            case Element.Earth:
                GameManager.player.IncreaseProgress(Player.Progress.EarthSkill, 1);
                break;
            case Element.Water:
                GameManager.player.IncreaseProgress(Player.Progress.WaterSkill, 1);
                break;
            case Element.Air:
                GameManager.player.IncreaseProgress(Player.Progress.AirSkill, 1);
                break;
        }
    }
}