using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class StageSelection : Navigation
{
    public bool isUnlocked;
    public bool firstAccess;
    public Map stage;

    public GameObject popUp;

    void Start()
    {
        if(firstAccess)
        {
            IsHovered(true);
        }
    }

    public override void IsHovered(bool state)
    {
        if(isUnlocked == false)
        {
            WindowsController.HoveredButton = Left;
        } else
        {
            if(state)
            {
                GetComponent<Image>().sprite = HoverSprite;
                popUp.SetActive(true);
            }
            else
            {
                GetComponent<Image>().sprite = BasicSprite;
                popUp.SetActive(false);
            }
        }

    }

    public override void Clicked()
    {
        if(isUnlocked)
        {
            SceneManager.LoadScene(""+stage);
        }
    }

    public override void ExclusiveKey()
    {

    }

}
