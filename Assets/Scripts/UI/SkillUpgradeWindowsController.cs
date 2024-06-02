using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeWindowsController : MonoBehaviour
{
    [SerializeField] private windowsController WindowsController;
    [SerializeField] private Text skillName, level1, desc1, level2, desc2, costText;
    [SerializeField] private Image skillImg, descImg1, descImg2;
    [SerializeField] private GameObject upgradeBtn, cancelBtn, price;
    private Skill skill;

    void Update()
    {
        if (gameObject.activeInHierarchy && WindowsController.HoveredButton == null)
        {
            skill = WindowsController.FocusedButton.GetComponent<SkillsSelection>().GetSkill();
            if (skill.CanBeUpgraded())
            {
                WindowsController.HoveredButton = upgradeBtn;
                upgradeBtn.SetActive(true);
                price.SetActive(true);
            }
            else
            {
                WindowsController.HoveredButton = cancelBtn;
                upgradeBtn.SetActive(false);
                price.SetActive(false);
            }
        }

        skillImg.sprite = skill.Sprite;
        skillName.text = skill.Name;

        level1.text = "Level " + skill.Level.ToString();
        desc1.text = "CD: " + skill.Cd + "\n" +
                     "Mana Cost: " + skill.Cost + "\n\n"
                     + skill.GetDescription();

        if (skill.CanBeUpgraded())
        {
            upgradeBtn.SetActive(true);
            price.SetActive(true);

            descImg1.gameObject.GetComponent<DescriptionBehavior>().Right = upgradeBtn;
            descImg2.gameObject.GetComponent<DescriptionBehavior>().Left = upgradeBtn;

            costText.color = GameManager.player.exp < skill.ExpUpCost ? Color.red : Color.white;
            costText.text = skill.ExpUpCost.ToString();

            Skill skill2 = skill.Clone();
            skill2.UpgradeLevel();
            level2.text = "Level " + skill2.Level.ToString();
            desc2.text = "CD: " + skill2.Cd + "\n" +
                         "Mana Cost: " + skill2.Cost + "\n\n"
                         + skill2.GetDescription();
        }
        else
        {
            if (WindowsController.HoveredButton == upgradeBtn)
            {
                WindowsController.HoveredButton = cancelBtn;
                cancelBtn.GetComponent<Navigation>().IsHovered(true);
                cancelBtn.GetComponent<Navigation>().Up = null;
            }

            descImg1.gameObject.GetComponent<DescriptionBehavior>().Right = cancelBtn;
            descImg2.gameObject.GetComponent<DescriptionBehavior>().Left = cancelBtn;

            // WindowsController.HoveredButton = WindowsController.HoveredButton == null ? cancelBtn : WindowsController.HoveredButton;
            upgradeBtn.SetActive(false);
            price.SetActive(false);

            level2.text = "Level Max";
            desc2.text = "-";
        }


    }
}