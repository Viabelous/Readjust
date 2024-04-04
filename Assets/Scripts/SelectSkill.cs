using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    // Start is called before the first frame update

    int totalSelected;
    bool selected = false;
    public GameObject skillObject;
    Image img;
    int slotNumber;
    void Start()
    {
        img = GetComponent<Image>();
        totalSelected = TotalSelected();
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
            Debug.Log("Tambah");
            slotNumber = totalSelected + 1;
            GameManager.playerNow.selectedSkills[TotalSelected()] = new Skill(skillObject.name);

            img.color = Color.blue;
        }

        else
        {
            Debug.Log("Hapus");
            Unselected();
            img.color = Color.white;
            // selected = false;

        }

        Debug.Log(TotalSelected());
    }

    void Unselected()
    {
        for (int i = slotNumber - 1; i < TotalSelected(); i++)
        {
            if (i == GameManager.playerNow.selectedSkills.Length - 1)
            {
                GameManager.playerNow.selectedSkills[i] = null;
            }
            else
            {
                GameManager.playerNow.selectedSkills[i] = GameManager.playerNow.selectedSkills[i + 1];
            }

        }
    }

    int TotalSelected()
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
