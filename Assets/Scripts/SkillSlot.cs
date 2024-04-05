using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    // public Sprite[] skillsImg;
    public Text cdText;
    public int slotNumber;
    public bool isEmpty;

    public bool isCooldown = false;

    private Image img;
    private GameObject objDark;
    private float maxCd, currCd, minCd;

    void Start()
    {
        img = GetComponent<Image>();
        objDark = GameObject.Find("skill_" + slotNumber.ToString() + "_dark");

        // slot ada skillnya
        if (slotNumber <= TotalSelectedSkills())
        {
            isEmpty = false;

            // ganti gambar sesuai skill yang dipakai
            int index = slotNumber - 1;
            img.sprite = GameManager.playerNow.selectedSkills[index].sprite;
            objDark.GetComponent<Image>().sprite = img.sprite;

            maxCd = GameManager.playerNow.selectedSkills[index].maxCd;
            currCd = 0;
            minCd = 0;
        }
        // slot kosong
        else
        {
            isEmpty = true;
            objDark.SetActive(false);
            cdText.text = "";
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!isEmpty)
        {

            if (isCooldown)
            {
                cdText.text = Math.Ceiling(currCd).ToString();
            }
            else
            {
                cdText.text = "";
            }

            // cooldown selesai
            if (currCd < minCd)
            {
                // baru selesai cooldown
                if (isCooldown)
                {
                    isCooldown = false;
                    GameManager.skillsAvailable[slotNumber - 1].isCooldown = false;
                    objDark.GetComponent<Image>().fillAmount = 0;
                }

                // player sudah menggunakan skill
                if (GameManager.playerNow.selectedSkills[slotNumber - 1].isCooldown)
                {
                    currCd = maxCd;
                    objDark.GetComponent<Image>().fillAmount = 1;

                    isCooldown = true;
                }
            }
            // lagi cooldown
            else
            {
                objDark.GetComponent<Image>().fillAmount = currCd / maxCd;
                currCd -= Time.deltaTime;
            }
        }

    }

    // ca
    // int SkillIndex()
    // {
    //     for (int i = 0; i < GameManager.playerNow.selectedSkills.Length; i++)
    //     {
    //         if ( == GameManager.playerNow.selectedSkills[slotNumber - 1].name)
    //         {
    //             return i;
    //         }
    //     }
    //     return -1;

    // }

    int TotalSelectedSkills()
    {
        int total = GameManager.playerNow.selectedSkills.Length;
        for (int i = 0; i < total; i++)
        {

            if (GameManager.playerNow.selectedSkills[i] == null)
            {
                return i;
            }
        }
        return total;
    }

    // bool isCooldown()
    // {
    //     img.color = Color.black;
    //     return false;
    // }
}
