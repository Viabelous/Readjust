using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsBtnSelection : Navigation
{
    enum BtnType
    {
        Select, OpenUpgrade, Upgrade, Unlock, Cancel
    }

    [SerializeField] private BtnType type;
    [SerializeField] private NavigationState currState;
    [SerializeField] private Text btnText, costText;
    private Color currentColor;
    private SkillsSelection focusedSkill;

    void Start()
    {
        currentColor = ImageComponent.color;
    }

    void Update()
    {

        focusedSkill = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

        // hasUnlocked = GameManager.CheckUnlockedSkill(Left.GetComponent<SkillsSelection>().GetSkill().Name);

        switch (type)
        {
            case BtnType.Select:
                Left = WindowsController.FocusedButton;
                if (Down != null && !Down.activeInHierarchy)
                {
                    Down = null;
                }
                else if (GameObject.Find("open_upgrade_btn") != null)
                {
                    Down = GameObject.Find("open_upgrade_btn");
                }

                if (Left.GetComponent<SkillsSelection>().HasSelected())
                {
                    btnText.text = "BATALKAN";
                }
                else
                {
                    btnText.text = "GUNAKAN";
                }

                break;

            case BtnType.OpenUpgrade:
                Left = WindowsController.FocusedButton;
                break;

            case BtnType.Unlock:
                Left = WindowsController.FocusedButton;

                costText.color = GameManager.player.exp < focusedSkill.GetSkill().ExpUnlockCost ? Color.red : Color.black;
                costText.text = focusedSkill.GetSkill().ExpUnlockCost.ToString();
                break;

            case BtnType.Cancel:
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

            // beli / buka skill baru --------------------------------------------------------
            case BtnType.Unlock:
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

            case BtnType.OpenUpgrade:
                currState = NavigationState.Active;
                WindowsController.HoveredButton = null;
                GameObject.FindObjectOfType<SkillWindowsController>().GetUpgradeWindow().SetActive(true);
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
                    GameManager.player.Pay(CostType.Exp, skill.ExpUpCost);
                    focusedSkill.Upgrade();
                }
                break;

            case BtnType.Cancel:
                currState = NavigationState.Active;
                GameObject.FindObjectOfType<SkillWindowsController>().GetUpgradeWindow().SetActive(false);
                WindowsController.HoveredButton = GameObject.Find("select_btn");
                break;
        }
    }

    public override void ExclusiveKey()
    {

    }

    // private void HoverBackToSkill(SkillsSelection focusedSkill)
    // {
    //     focusedSkill.ChangeCurrentState(NavigationState.Hover);
    //     WindowsController.HoveredButton = Left;
    //     WindowsController.FocusedButton = null;
    //     currState = NavigationState.Active;
    // }

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