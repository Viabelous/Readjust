using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject selectBtn, upgradeBtn, locked, lockedBtn;
    [SerializeField] private Text skillName, skillCD, skillCost, skillDescription;
    SkillsSelection skillsSelection;

    void Start()
    {
        UpdateSkillBtnHover();
        // SetSkillDescription();
    }

    void Update()
    {
        UpdateSkillBtnHover();
        SetSkillDescription();

        // sesudah upgrade skill, hover ke arah select btn
        if (
            skillsSelection.CurrentState() == NavigationState.Focused &&
            WindowsController.HoveredButton == lockedBtn &&
            !lockedBtn.activeInHierarchy
        )
        {
            WindowsController.HoveredButton = selectBtn;
        }
    }

    void SetSkillDescription()
    {

        selectBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        upgradeBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        locked.SetActive(skillsSelection.hasUnlocked ? false : true);

        if (skillsSelection.GetSkill().Level == skillsSelection.GetSkill().MaxLevel)
        {
            upgradeBtn.SetActive(false);
        }

        skillName.text = skillsSelection.GetSkill().Name + " (Lv. " + skillsSelection.GetSkill().Level.ToString() + ")";
        skillCD.text = "CD: " + skillsSelection.GetSkill().Cd;
        skillCost.text = "Mana Cost: " + skillsSelection.GetSkill().Cost;
        skillDescription.text = skillsSelection.GetSkill().GetDescription();
    }

    void UpdateSkillBtnHover()
    {
        if (WindowsController.FocusedButton == null)
        {
            skillsSelection = WindowsController.HoveredButton.GetComponent<SkillsSelection>();
        }
        else
        {
            skillsSelection = WindowsController.FocusedButton.GetComponent<SkillsSelection>();
        }
    }
}