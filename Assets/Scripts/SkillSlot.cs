using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public GameObject objLight, objDark, skillHolder;
    public Text cdText;
    public int slotNumber;
    public SkillState state;

    [HideInInspector]
    public GameObject skillPref;

    [HideInInspector]
    public Skill skill;

    [HideInInspector]
    public bool isEmpty;

    private PlayerController playerController;
    private float maxCd, currCd, minCd;

    void Start()
    {
        // objLight = GameObject.Find("skill_" + slotNumber.ToString());
        // objDark = GameObject.Find("skill_" + slotNumber.ToString() + "_dark");

        // slot ada skillnya
        if (slotNumber <= GameManager.selectedSkills.Count)
        {
            isEmpty = false;
            playerController = GameObject.FindObjectOfType<PlayerController>();

            // ganti gambar sesuai skill yang dipakai
            int index = slotNumber - 1;
            skillPref = SkillHolder.Instance.skillPrefs.Find(prefab => prefab.GetComponent<SkillController>().skill.name == GameManager.selectedSkills[index]);
            skill = skillPref.GetComponent<SkillController>().skill;

            objLight.GetComponent<Image>().sprite = skill.Sprite;
            objDark.GetComponent<Image>().sprite = objLight.GetComponent<Image>().sprite;

            maxCd = skill.Cd;
            currCd = 0;
            minCd = 0;
            state = SkillState.ready;
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

            switch (state)
            {
                case SkillState.ready:
                    cdText.text = "";
                    objDark.GetComponent<Image>().fillAmount = 0;

                    if (
                        Input.inputString == slotNumber.ToString() &&
                       // jika bayarannya mana & mana yg tersedia > bayaran
                       (skill.CostType == SkillCost.Mana && playerController.player.mana > skill.Cost ||
                        // jika bayarannya hp & hp yg tersedia > bayaran + 1% dari total hp keseluruhan
                        skill.CostType == SkillCost.Hp && playerController.player.hp > skill.Cost + playerController.player.hp * 00.1)
                    )
                    {
                        Instantiate(skillPref);
                        playerController.UseSkill(skill);
                        state = SkillState.active;
                    }
                    break;

                case SkillState.active:
                    state = SkillState.cooldown;
                    objDark.GetComponent<Image>().fillAmount = 1;
                    currCd = maxCd;
                    break;

                case SkillState.cooldown:
                    currCd -= Time.deltaTime;
                    cdText.text = Math.Ceiling(currCd).ToString();

                    objDark.GetComponent<Image>().fillAmount = currCd / maxCd;

                    if (currCd <= minCd)
                    {
                        state = SkillState.ready;
                    }

                    break;

            }

            // // cooldown selesai
            // if (currCd < minCd)
            // {
            //     // baru selesai cooldown
            //     if (isCooldown)
            //     {
            //         isCooldown = false;
            //         GameManager.selectedSkills[slotNumber - 1].isCooldown = false;
            //         objDark.GetComponent<Image>().fillAmount = 0;
            //     }

            //     // player sudah menggunakan skill
            //     if (GameManager.selectedSkills[slotNumber - 1].isCooldown)
            //     {
            //         currCd = maxCd;
            //         objDark.GetComponent<Image>().fillAmount = 1;

            //         isCooldown = true;
            //     }
            // }
            // // lagi cooldown
            // else
            // {
            //     objDark.GetComponent<Image>().fillAmount = currCd / maxCd;
            //     currCd -= Time.deltaTime;
            // }
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

    // int TotalSelectedSkills()
    // {
    //     int total = GameManager.playerNow.selectedSkills.Length;
    //     for (int i = 0; i < total; i++)
    //     {

    //         if (GameManager.playerNow.selectedSkills[i] == null)
    //         {
    //             return i;
    //         }
    //     }
    //     return total;
    // }

    // bool isCooldown()
    // {
    //     img.color = Color.black;
    //     return false;
    // }
}
