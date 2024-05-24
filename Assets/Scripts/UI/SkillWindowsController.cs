using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject selectBtn, upgradeBtn, lockedBtn;
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

        // kalau sudah dibuka, maka tampilkan tombol select
        selectBtn.SetActive(skillsSelection.hasUnlocked ? true : false);

        // kalau skill yg dihover sudah mencapai level maksimal,
        // maka sembunyikan tombol upgrade
        if (skillsSelection.GetSkill().Level == skillsSelection.GetSkill().MaxLevel)
        {
            upgradeBtn.SetActive(false);
        }
        else
        {
            upgradeBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        }

        // kalau skill yg dihover belum dapat dibeli atau progres skill elemen
        // belum terpenuhi, maka sembunyikan tombol unlock
        if (!skillsSelection.GetSkill().CanBeUnlocked(GameManager.player))
        {
            lockedBtn.SetActive(false);
        }
        else
        {
            // kalau sudah dibuka, maka sembunyikan tombol unlocked
            lockedBtn.SetActive(skillsSelection.hasUnlocked ? false : true);
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