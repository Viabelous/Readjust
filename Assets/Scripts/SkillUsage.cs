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

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip fireSound, earthSound, waterSound, airSound;

    void Start()
    {

        // slot ada skillnya
        if (slotNumber <= GameManager.selectedSkills.Count)
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

    public void PlaySound()
    {
        switch (skill.Element)
        {
            case Element.Fire:
                audioSource.clip = fireSound;
                break;
            case Element.Earth:
                audioSource.clip = earthSound;
                break;
            case Element.Water:
                audioSource.clip = waterSound;
                break;
            case Element.Air:
                audioSource.clip = airSound;
                break;
        }

        audioSource.Play();
    }
}
