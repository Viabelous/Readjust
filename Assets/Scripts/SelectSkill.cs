using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    // Start is called before the first frame update


    bool selected = false;
    Image img;
    int slotNumber;

    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (img.color == Color.blue)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }

        if (!selected)
        {
            // int totalSelected = GameManager.playerNow.selectedSkills.Count;
            // Debug.Log("Tambah");
            // slotNumber = totalSelected + 1;
            // GameManager.playerNow.selectedSkills[totalSelected] = GameManager.skills[SkillIndex()];
            GameManager.playerNow.selectedSkills.Add(
                GameManager.skills.Find(skill => skill.name == gameObject.name)
            );

            img.color = Color.blue;
        }

        else
        {
            Debug.Log("Hapus");
            // Unselected();
            GameManager.playerNow.selectedSkills.Remove(
                GameManager.skills.Find(skill => skill.name == gameObject.name)
            );
            img.color = Color.white;

        }


    }

    // void Unselected()
    // {
    //     for (int i = slotNumber - 1; i < TotalSelectedSkills(); i++)
    //     {
    //         if (i == GameManager.playerNow.selectedSkills.Length - 1)
    //         {
    //             GameManager.playerNow.selectedSkills[i] = null;
    //         }
    //         else
    //         {
    //             GameManager.playerNow.selectedSkills[i] = GameManager.playerNow.selectedSkills[i + 1];
    //         }

    //     }
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

    // int SkillIndex()
    // {
    //     for (int i = 0; i < GameManager.skills.Length; i++)
    //     {
    //         if (GameManager.skills[i].name == gameObject.name)
    //         {
    //             return i;
    //         }
    //     }
    //     return -1;
    // }
}
