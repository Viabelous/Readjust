using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Sprite[] skillsImg;
    public int slotNumber;

    private Image img;
    void Start()
    {
        img = GetComponent<Image>();

        // ganti gambar sesuai skill yang dipakai
        if (slotNumber <= TotalSelectedSkills())
        {
            img.sprite = skillsImg[SkillIndex()];

        }
    }

    // Update is called once per frame
    void Update()
    {
        // print(skillsImg[0].name);
        // print(GameManager.playerNow.selectedSkills[slotNumber - 1].name);

    }

    // ca
    int SkillIndex()
    {
        for (int i = 0; i < skillsImg.Length; i++)
        {
            if (skillsImg[i].name == GameManager.playerNow.selectedSkills[slotNumber - 1].name)
            {
                return i;
            }
        }
        return -1;

    }

    int TotalSelectedSkills()
    {
        for (int i = 0; i < GameManager.playerNow.selectedSkills.Length; i++)
        {

            if (GameManager.playerNow.selectedSkills[i] == null)
            {
                return i;
            }
            else
            {
                Debug.Log(GameManager.playerNow.selectedSkills[i].name);

            }
        }
        return 7;
    }
}
