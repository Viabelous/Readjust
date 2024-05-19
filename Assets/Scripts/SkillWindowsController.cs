using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject selectBtn, upgradeBtn, locked, lockedBtn;
    [SerializeField] private Text skillName, skillDescription;
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
        skillsSelection = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

        skillName.text = skillsSelection.GetSkill().Name + " (Lv. " + skillsSelection.GetSkill().Level.ToString() + ")";
        skillDescription.text = skillsSelection.GetSkill().Description;
    }
}