using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private Text skillName, level1, desc1, level2, desc2, costText;
    [SerializeField] private Image skillImg;
    [SerializeField] private GameObject upgradeBtn, cancelBtn, price;
    private Skill skill;

    void Start()
    {

    }
    void Update()
    {
        if (gameObject.activeInHierarchy && WindowsController.HoveredButton == null)
        {
            skill = WindowsController.FocusedButton.GetComponent<SkillsSelection>().GetSkill();
            if (skill.CanBeUpgraded())
            {
                upgradeBtn.SetActive(true);
                price.SetActive(true);
                WindowsController.HoveredButton = upgradeBtn;
            }
            else
            {
                upgradeBtn.SetActive(false);
                price.SetActive(false);
                WindowsController.HoveredButton = cancelBtn;

            }
        }

        skillImg.sprite = skill.Sprite;
        skillName.text = skill.Name;

        level1.text = "Level " + skill.Level.ToString();
        desc1.text = skill.GetDescription();

        if (skill.CanBeUpgraded())
        {
            upgradeBtn.SetActive(true);
            price.SetActive(true);

            costText.color = GameManager.player.exp < skill.ExpUpCost ? Color.red : Color.white;
            costText.text = skill.ExpUpCost.ToString();

            Skill skill2 = skill.Clone();
            skill2.UpgradeLevel();
            level2.text = "Level " + skill2.Level.ToString();
            desc2.text = skill2.GetDescription();
        }
        else
        {
            WindowsController.HoveredButton = cancelBtn;
            upgradeBtn.SetActive(false);
            price.SetActive(false);

            level2.text = "Level Max";
            desc2.text = "-";
        }


    }
}