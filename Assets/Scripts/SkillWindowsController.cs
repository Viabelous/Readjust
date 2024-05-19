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
    }

    void Update()
    {
        SetSkillDescription();
        // skillLevel.text = skillsSelection.GetSkill().Level.ToString();

        selectBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        upgradeBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        locked.SetActive(skillsSelection.hasUnlocked ? false : true);
    }

    void SetSkillDescription()
    {

        if (WindowsController.FocusedButton == null)
        {
            skillsSelection = WindowsController.HoveredButton.GetComponent<SkillsSelection>();
        }
        else
        {
            skillsSelection = WindowsController.FocusedButton.GetComponent<SkillsSelection>();
        }

        if (skillsSelection.GetSkill().Level == skillsSelection.GetSkill().MaxLevel)
        {
            upgradeBtn.SetActive(false);
        }

        skillName.text = skillsSelection.GetSkill().Name + " (Lv. " + skillsSelection.GetSkill().Level.ToString() + ")";
        skillCD.text = "CD: " + skillsSelection.GetSkill().Cd;
        skillCost.text = "Mana Cost: " + skillsSelection.GetSkill().Cost;
        skillDescription.text = skillsSelection.GetSkill().GetDescription();
    }
}