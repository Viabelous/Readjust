using UnityEngine.UI;
using UnityEngine;

public class SelectedSkillSlot : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    [HideInInspector] private GameObject prefab;
    [SerializeField] private Image slotImg;
    private Sprite initialImage;

    private int index;

    void Start()
    {
        initialImage = slotImg.GetComponent<Image>().sprite;
        ChangeImageSkill();
    }

    void Update()
    {
        ChangeImageSkill();
    }

    private void ChangeImageSkill()
    {
        if (slotNumber <= GameManager.selectedSkills.Count)
        {
            index = slotNumber - 1;
            prefab = GameManager.selectedSkills[index];
            slotImg.GetComponent<Image>().sprite = prefab.GetComponent<SkillController>().playerSkill.Sprite;
        }
        else
        {
            slotImg.GetComponent<Image>().sprite = initialImage;
        }
    }
}