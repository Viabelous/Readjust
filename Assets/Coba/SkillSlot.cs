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
        img.sprite = skillsImg[SkillIndex()];
    }

    // Update is called once per frame
    void Update()
    {
        // print(skillsImg[0].name);
        // print(GameManager.playerNow.usedSkills[slotNumber - 1].name);

    }

    // ca
    int SkillIndex()
    {
        for (int i = 0; i < skillsImg.Length; i++)
        {
            if (skillsImg[i].name == GameManager.playerNow.usedSkills[slotNumber - 1].name)
            {
                return i;
            }
        }
        return -1;

    }
}
