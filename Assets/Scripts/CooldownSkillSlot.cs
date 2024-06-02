using System;
using UnityEngine;
using UnityEngine.UI;
public enum SkillState
{
    Ready,
    Active,
    Cooldown
}

public class SkillUsage : MonoBehaviour
{
    public GameObject objLight, objDark, skillHolder;
    public Text cdText;
    public int slotNumber;

    [HideInInspector]
    public GameObject skillPref;
    // public GameObject skillPref;

    [HideInInspector]
    public Skill skill;

    [HideInInspector]
    public bool isEmpty;

    private SkillState state;
    private PlayerController playerController;
    private float maxCd, currCd, minCd;

    void Start()
    {

        // slot ada skillnya

        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        if (slotNumber <= GameManager.selectedSkills.Count)
        // if (slotNumber <= CumaBuatDebug.instance.selectedSkills.Count)
        {
            isEmpty = false;
            playerController = GameObject.FindObjectOfType<PlayerController>();

            // ganti gambar sesuai skill yang dipakai
            int index = slotNumber - 1;

            skillPref = GameManager.selectedSkills[index];
            // skillPref = CumaBuatDebug.instance.selectedSkills[index];
            skill = skillPref.GetComponent<SkillController>().skill;

            objLight.GetComponent<Image>().sprite = skill.Sprite;
            objDark.GetComponent<Image>().sprite = objLight.GetComponent<Image>().sprite;

            maxCd = skill.Cd;
            currCd = 0;
            minCd = 0;
            state = SkillState.Ready;
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
        if (!isEmpty && StageManager.instance.CurrentState() == StageState.Play)
        {
            switch (state)
            {
                case SkillState.Ready:
                    cdText.text = "";
                    objDark.GetComponent<Image>().fillAmount = 0;

                    if (
                        Input.inputString == slotNumber.ToString() &&
                       // jika bayarannya mana & mana yg tersedia > bayaran
                       (skill.CostType == CostType.Mana && playerController.player.mana > skill.Cost ||
                        // jika bayarannya hp & hp yg tersedia > bayaran + 1% dari total hp keseluruhan
                        skill.CostType == CostType.Hp && playerController.player.hp > playerController.player.hp * 0.1)
                    )
                    {
                        Instantiate(skillPref);
                        // skill.Activate(skillPref);

                    }
                    break;

                case SkillState.Active:
                    objDark.GetComponent<Image>().fillAmount = 1;
                    currCd = maxCd;

                    ChangeState(SkillState.Cooldown);
                    break;

                case SkillState.Cooldown:
                    currCd -= Time.deltaTime;
                    cdText.text = Math.Ceiling(currCd).ToString();

                    objDark.GetComponent<Image>().fillAmount = currCd / maxCd;

                    if (currCd <= minCd)
                    {
                        ChangeState(SkillState.Ready);
                    }

                    break;

            }
        }

    }

    public void ChangeState(SkillState newState)
    {
        state = newState;
    }
}
