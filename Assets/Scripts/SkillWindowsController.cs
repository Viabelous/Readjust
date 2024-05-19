using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject selectBtn, upgradeBtn, locked, lockedBtn;
    SkillsSelection skillsSelection;

    void Start()
    {

    }

    void Update()
    {
        skillsSelection = WindowsController.FocusedButton.GetComponent<SkillsSelection>();

        selectBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        upgradeBtn.SetActive(skillsSelection.hasUnlocked ? true : false);
        locked.SetActive(skillsSelection.hasUnlocked ? false : true);
    }
}